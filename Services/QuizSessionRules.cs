namespace quizApi.Services;

/// <summary>Tur başına soru sayısı (havuzdan rastgele) ve tüm tur için toplam süre (saniye).</summary>
public static class QuizSessionRules
{
    public const int SecondsPerRound = 60;

    public const int QuestionsPerRoundStandard = 15;

    public const int QuestionsPerRoundBilmece = 20;

    public static int QuestionsPerRound(string categorySlug) =>
        categorySlug.Equals("bilmece", StringComparison.OrdinalIgnoreCase)
            ? QuestionsPerRoundBilmece
            : QuestionsPerRoundStandard;

    public static int PickCount(int poolSize, string categorySlug)
    {
        var want = QuestionsPerRound(categorySlug);
        return poolSize <= 0 ? 0 : Math.Min(want, poolSize);
    }
}
