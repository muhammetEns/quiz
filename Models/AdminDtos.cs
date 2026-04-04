namespace quizApi.Models;

public record AdminQuizSummaryDto(int Id, string CategorySlug, string Title, string? Description, int QuestionCount);

public record AdminQuestionDto(
    int Id,
    int QuizId,
    string Text,
    IReadOnlyList<string> Options,
    int CorrectOptionIndex,
    int SortOrder);

public record AdminQuestionCreateDto(string Text, string[] Options, int CorrectOptionIndex, int? SortOrder);

public record AdminQuestionUpdateDto(string Text, string[] Options, int CorrectOptionIndex, int? SortOrder);
