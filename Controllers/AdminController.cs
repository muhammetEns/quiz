using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using quizApi.Data;
using quizApi.Models;

namespace quizApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AdminController : ControllerBase
{
    private readonly QuizDbContext _db;

    public AdminController(QuizDbContext db) => _db = db;

    [HttpGet("quizzes")]
    [ProducesResponseType(typeof(IReadOnlyList<AdminQuizSummaryDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<AdminQuizSummaryDto>>> ListQuizzes(CancellationToken ct)
    {
        var list = await _db.Quizzes.AsNoTracking()
            .OrderBy(q => q.Id)
            .Select(q => new AdminQuizSummaryDto(q.Id, q.CategorySlug, q.Title, q.Description, q.Questions.Count))
            .ToListAsync(ct);
        return Ok(list);
    }

    [HttpGet("quizzes/{quizId:int}/questions")]
    [ProducesResponseType(typeof(IReadOnlyList<AdminQuestionDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IReadOnlyList<AdminQuestionDto>>> ListQuestions(int quizId, CancellationToken ct)
    {
        if (!await _db.Quizzes.AnyAsync(q => q.Id == quizId, ct)) return NotFound();

        var rows = await _db.Questions.AsNoTracking()
            .Where(q => q.QuizId == quizId)
            .OrderBy(q => q.SortOrder)
            .ThenBy(q => q.Id)
            .ToListAsync(ct);

        var dtos = rows.Select(q => new AdminQuestionDto(
            q.Id,
            q.QuizId,
            q.Text,
            q.OptionsList(),
            q.CorrectOptionIndex,
            q.SortOrder)).ToList();

        return Ok(dtos);
    }

    [HttpPost("quizzes/{quizId:int}/questions")]
    [ProducesResponseType(typeof(AdminQuestionDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<AdminQuestionDto>> CreateQuestion(int quizId, [FromBody] AdminQuestionCreateDto? body, CancellationToken ct)
    {
        if (body is null) return BadRequest();
        var err = ValidateQuestionPayload(body.Text, body.Options, body.CorrectOptionIndex);
        if (err is not null) return BadRequest(new { message = err });

        var quiz = await _db.Quizzes.FirstOrDefaultAsync(q => q.Id == quizId, ct);
        if (quiz is null) return NotFound();

        var sort = body.SortOrder ?? await _db.Questions.Where(q => q.QuizId == quizId).Select(q => q.SortOrder).DefaultIfEmpty(-1).MaxAsync(ct) + 1;

        var row = new QuestionRow
        {
            QuizId = quizId,
            Text = body.Text.Trim(),
            Option0 = body.Options[0].Trim(),
            Option1 = body.Options[1].Trim(),
            Option2 = body.Options[2].Trim(),
            Option3 = body.Options[3].Trim(),
            CorrectOptionIndex = body.CorrectOptionIndex,
            SortOrder = sort
        };
        _db.Questions.Add(row);
        await _db.SaveChangesAsync(ct);

        var dto = new AdminQuestionDto(row.Id, row.QuizId, row.Text, row.OptionsList(), row.CorrectOptionIndex, row.SortOrder);
        return CreatedAtAction(nameof(ListQuestions), new { quizId }, dto);
    }

    [HttpPut("questions/{questionId:int}")]
    [ProducesResponseType(typeof(AdminQuestionDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<AdminQuestionDto>> UpdateQuestion(int questionId, [FromBody] AdminQuestionUpdateDto? body, CancellationToken ct)
    {
        if (body is null) return BadRequest();
        var err = ValidateQuestionPayload(body.Text, body.Options, body.CorrectOptionIndex);
        if (err is not null) return BadRequest(new { message = err });

        var row = await _db.Questions.FirstOrDefaultAsync(q => q.Id == questionId, ct);
        if (row is null) return NotFound();

        row.Text = body.Text.Trim();
        row.Option0 = body.Options[0].Trim();
        row.Option1 = body.Options[1].Trim();
        row.Option2 = body.Options[2].Trim();
        row.Option3 = body.Options[3].Trim();
        row.CorrectOptionIndex = body.CorrectOptionIndex;
        if (body.SortOrder is not null) row.SortOrder = body.SortOrder.Value;

        await _db.SaveChangesAsync(ct);

        return Ok(new AdminQuestionDto(row.Id, row.QuizId, row.Text, row.OptionsList(), row.CorrectOptionIndex, row.SortOrder));
    }

    [HttpDelete("questions/{questionId:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteQuestion(int questionId, CancellationToken ct)
    {
        var row = await _db.Questions.FirstOrDefaultAsync(q => q.Id == questionId, ct);
        if (row is null) return NotFound();
        _db.Questions.Remove(row);
        await _db.SaveChangesAsync(ct);
        return NoContent();
    }

    private static string? ValidateQuestionPayload(string text, string[]? options, int correctIndex)
    {
        if (string.IsNullOrWhiteSpace(text)) return "Soru metni gerekli.";
        if (options is null || options.Length != 4) return "Tam dört şık gerekli.";
        for (var i = 0; i < 4; i++)
        {
            if (string.IsNullOrWhiteSpace(options[i])) return $"Şık {i + 1} boş olamaz.";
        }

        if (correctIndex < 0 || correctIndex > 3) return "Doğru şık 0–3 arası olmalı.";
        return null;
    }
}
