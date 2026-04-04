namespace quizApi;

/// <summary>Oyun puan kuralları: doğru +2, yanlış -1; toplam puan bu eşiğe veya altına inince oyun biter.</summary>
public static class ScoringRules
{
    public const int PointsPerCorrect = 2;
    public const int PointsPerWrong = -1;
    public const int LoseScoreThreshold = -10;
}
