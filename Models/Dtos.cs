namespace quizApi.Models;

public record QuizSummaryDto(int Id, string CategorySlug, string Title, string? Description, int QuestionCount, int QuestionsPerRound);

public record QuestionPlayDto(int Id, string Text, IReadOnlyList<string> Options);

public record QuizPlayDto(int Id, string CategorySlug, string Title, string? Description, int SecondsPerRound, IReadOnlyList<QuestionPlayDto> Questions);

public record SubmitAnswerDto(int QuestionId, int SelectedOptionIndex);

public record SubmitQuizRequest(string? PlayerName, IReadOnlyList<int>? SessionQuestionIds, IReadOnlyList<SubmitAnswerDto> Answers);

public record LeaderboardEntryDto(int Rank, string PlayerName, int FinalScore, int CorrectCount, DateTimeOffset PlayedAt);

public record CheckAnswerRequest(int SelectedOptionIndex);

public record CheckAnswerResponse(bool IsCorrect);

/// <param name="Answered">Bu soru için cevap gönderildi mi?</param>
/// <param name="PointsDelta">Puan etkisi: +2, -1 veya oyun bittiği için sayılmadıysa 0.</param>
/// <param name="CountedForScore">Bu sorunun puanı toplama dahil edildi mi?</param>
public record AnswerResultDto(
    int QuestionId,
    bool IsCorrect,
    int CorrectOptionIndex,
    bool Answered,
    int PointsDelta,
    int RunningScoreAfter,
    bool CountedForScore);

/// <param name="GameLost">Puan -10 veya altına düştüyse true (kural: bu eşikte kaybedersiniz).</param>
public record SubmitQuizResponse(
    int FinalScore,
    bool GameLost,
    int? LostAfterQuestionId,
    int CorrectCount,
    int AnsweredCount,
    int TotalQuestions,
    IReadOnlyList<AnswerResultDto> Results);
