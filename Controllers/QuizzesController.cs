using Microsoft.AspNetCore.Mvc;
using quizApi.Data;
using quizApi.Models;
using quizApi.Services;

namespace quizApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class QuizzesController : ControllerBase
{
    private readonly IQuizStore _store;
    private readonly QuizDbContext _db;

    public QuizzesController(IQuizStore store, QuizDbContext db)
    {
        _store = store;
        _db = db;
    }

    /// <summary>Tüm quiz'lerin özet listesi (soru sayısı dahil, cevap yok).</summary>
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<QuizSummaryDto>), StatusCodes.Status200OK)]
    public ActionResult<IReadOnlyList<QuizSummaryDto>> GetAll() => Ok(_store.ListSummaries());

    /// <summary>Belirli bir quiz'i oynamak için sorular ve şıklar (doğru cevap gönderilmez).</summary>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(QuizPlayDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<QuizPlayDto> GetForPlay(int id)
    {
        var dto = _store.GetForPlay(id);
        return dto is null ? NotFound() : Ok(dto);
    }

    /// <summary>Tek soruda seçilen şıkkın doğruluğu (oyun sırasında puan güncellemek için).</summary>
    [HttpPost("{id:int}/questions/{questionId:int}/check")]
    [ProducesResponseType(typeof(CheckAnswerResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<CheckAnswerResponse> CheckAnswer(int id, int questionId, [FromBody] CheckAnswerRequest? body)
    {
        if (body is null) return BadRequest();
        var ok = _store.CheckSingleAnswer(id, questionId, body.SelectedOptionIndex);
        return ok is null ? NotFound() : Ok(new CheckAnswerResponse(ok.Value));
    }

    /// <summary>Gönderilen cevapları sırayla puanlar: doğru +2, yanlış -1; puan -10 veya altına inince oyun kaybedilmiş sayılır.</summary>
    [HttpPost("{id:int}/submit")]
    [ProducesResponseType(typeof(SubmitQuizResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<SubmitQuizResponse>> Submit(int id, [FromBody] SubmitQuizRequest? body)
    {
        if (body?.Answers is null || body.Answers.Count == 0)
            return BadRequest(new { message = "En az bir cevap gönderilmelidir." });

        var result = _store.Grade(id, body.Answers, body.SessionQuestionIds);
        if (result is null) return NotFound();

        if (!string.IsNullOrWhiteSpace(body.PlayerName))
        {
            var name = body.PlayerName.Trim();
            if (name.Length > 80) name = name[..80];
            var completedAll = !result.GameLost && result.AnsweredCount == result.TotalQuestions;
            _db.GameScores.Add(new GameScore
            {
                PlayerName = name,
                QuizId = id,
                FinalScore = result.FinalScore,
                GameLost = result.GameLost,
                CompletedAll = completedAll,
                CorrectCount = result.CorrectCount,
                PlayedAt = DateTimeOffset.UtcNow
            });
            await _db.SaveChangesAsync(HttpContext.RequestAborted);
        }

        return Ok(result);
    }
}
