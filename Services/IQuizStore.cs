using quizApi.Models;

namespace quizApi.Services;

public interface IQuizStore
{
    IReadOnlyList<QuizSummaryDto> ListSummaries();
    QuizPlayDto? GetForPlay(int quizId);
    SubmitQuizResponse? Grade(int quizId, IReadOnlyList<SubmitAnswerDto> answers, IReadOnlyList<int>? sessionQuestionOrder);
    /// <summary>Tek bir şıkkın doğruluğu (canlı puan için). Quiz veya soru yoksa null.</summary>
    bool? CheckSingleAnswer(int quizId, int questionId, int selectedOptionIndex);
}
