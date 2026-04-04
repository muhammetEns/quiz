using Microsoft.EntityFrameworkCore;
using quizApi.Data;
using quizApi.Models;

namespace quizApi.Services;

public class DatabaseQuizStore : IQuizStore
{
    private readonly QuizDbContext _db;

    public DatabaseQuizStore(QuizDbContext db) => _db = db;

    public IReadOnlyList<QuizSummaryDto> ListSummaries()
    {
        var rows = _db.Quizzes.AsNoTracking()
            .OrderBy(q => q.Id)
            .Select(q => new
            {
                q.Id,
                q.CategorySlug,
                q.Title,
                q.Description,
                Pool = q.Questions.Count
            })
            .ToList();

        return rows
            .Select(q => new QuizSummaryDto(
                q.Id,
                q.CategorySlug,
                q.Title,
                q.Description,
                q.Pool,
                QuizSessionRules.PickCount(q.Pool, q.CategorySlug)))
            .ToList();
    }

    public QuizPlayDto? GetForPlay(int quizId)
    {
        var quiz = _db.Quizzes.AsNoTracking()
            .Include(q => q.Questions)
            .FirstOrDefault(q => q.Id == quizId);
        if (quiz is null) return null;

        var pool = quiz.Questions.ToList();
        var pick = QuizSessionRules.PickCount(pool.Count, quiz.CategorySlug);

        var shuffled = pool.OrderBy(_ => Random.Shared.Next()).ToList();
        var picked = shuffled.Take(pick).ToList();

        var dtos = picked
            .Select(x => new QuestionPlayDto(x.Id, x.Text, x.OptionsList()))
            .ToList();

        return new QuizPlayDto(
            quiz.Id,
            quiz.CategorySlug,
            quiz.Title,
            quiz.Description,
            QuizSessionRules.SecondsPerRound,
            dtos);
    }

    public bool? CheckSingleAnswer(int quizId, int questionId, int selectedOptionIndex)
    {
        var row = _db.Questions.AsNoTracking()
            .FirstOrDefault(x => x.Id == questionId && x.QuizId == quizId);
        if (row is null) return null;
        if (selectedOptionIndex < 0 || selectedOptionIndex > 3) return false;
        return selectedOptionIndex == row.CorrectOptionIndex;
    }

    public SubmitQuizResponse? Grade(int quizId, IReadOnlyList<SubmitAnswerDto> answers, IReadOnlyList<int>? sessionQuestionOrder)
    {
        var quiz = _db.Quizzes.AsNoTracking()
            .Include(q => q.Questions)
            .FirstOrDefault(q => q.Id == quizId);
        if (quiz is null) return null;

        var byId = quiz.Questions.ToDictionary(x => x.Id);
        List<QuestionRow> ordered;

        if (sessionQuestionOrder is { Count: > 0 })
        {
            ordered = [];
            foreach (var qid in sessionQuestionOrder)
            {
                if (byId.TryGetValue(qid, out var row))
                    ordered.Add(row);
            }
        }
        else
        {
            ordered = quiz.Questions.OrderBy(x => x.SortOrder).ThenBy(x => x.Id).ToList();
        }

        if (ordered.Count == 0) return null;

        var results = new List<AnswerResultDto>();
        var score = 0;
        var lost = false;
        int? lostAfterQuestionId = null;
        var correct = 0;
        var answeredCount = 0;

        foreach (var q in ordered)
        {
            var submitted = answers.FirstOrDefault(a => a.QuestionId == q.Id);
            var answered = submitted is not null;

            if (!answered)
            {
                results.Add(new AnswerResultDto(
                    q.Id,
                    false,
                    q.CorrectOptionIndex,
                    Answered: false,
                    PointsDelta: 0,
                    RunningScoreAfter: score,
                    CountedForScore: false));
                continue;
            }

            answeredCount++;
            var isCorrect = submitted!.SelectedOptionIndex >= 0
                && submitted.SelectedOptionIndex <= 3
                && submitted.SelectedOptionIndex == q.CorrectOptionIndex;

            if (lost)
            {
                results.Add(new AnswerResultDto(
                    q.Id,
                    isCorrect,
                    q.CorrectOptionIndex,
                    Answered: true,
                    PointsDelta: 0,
                    RunningScoreAfter: score,
                    CountedForScore: false));
                continue;
            }

            var delta = isCorrect ? quizApi.ScoringRules.PointsPerCorrect : quizApi.ScoringRules.PointsPerWrong;
            score += delta;
            if (isCorrect) correct++;

            results.Add(new AnswerResultDto(
                q.Id,
                isCorrect,
                q.CorrectOptionIndex,
                Answered: true,
                PointsDelta: delta,
                RunningScoreAfter: score,
                CountedForScore: true));

            if (score <= quizApi.ScoringRules.LoseScoreThreshold)
            {
                lost = true;
                lostAfterQuestionId ??= q.Id;
            }
        }

        return new SubmitQuizResponse(
            score,
            lost,
            lostAfterQuestionId,
            correct,
            answeredCount,
            ordered.Count,
            results);
    }
}
