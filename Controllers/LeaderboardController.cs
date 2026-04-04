using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using quizApi.Data;
using quizApi.Models;

namespace quizApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LeaderboardController : ControllerBase
{
    private readonly QuizDbContext _db;

    public LeaderboardController(QuizDbContext db) => _db = db;

    /// <summary>Oyunu kaybetmeden bitirenler arasından en yüksek puana göre sıralı liste. <paramref name="quizId"/> zorunludur (kategori = quiz kimliği).</summary>
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<LeaderboardEntryDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<LeaderboardEntryDto>>> Get(
        [FromQuery] int? quizId,
        [FromQuery] int take = 15)
    {
        take = Math.Clamp(take, 1, 50);
        if (quizId is null or <= 0)
            return Ok(Array.Empty<LeaderboardEntryDto>());

        var q = _db.GameScores.AsNoTracking()
            .Where(x => !x.GameLost && x.CompletedAll && x.QuizId == quizId);

        // SQLite, DateTimeOffset ile ORDER BY çevirmediği için sıralama bellekte (filtre DB'de).
        var rows = await q.ToListAsync(HttpContext.RequestAborted);
        var sorted = rows
            .OrderByDescending(x => x.FinalScore)
            .ThenByDescending(x => x.CorrectCount)
            .ThenBy(x => x.PlayedAt)
            .Take(take)
            .ToList();

        var list = sorted
            .Select((x, i) => new LeaderboardEntryDto(i + 1, x.PlayerName, x.FinalScore, x.CorrectCount, x.PlayedAt))
            .ToList();

        return Ok(list);
    }
}
