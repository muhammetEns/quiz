namespace quizApi.Data;

public class GameScore
{
    public int Id { get; set; }
    public string PlayerName { get; set; } = string.Empty;
    public int QuizId { get; set; }
    public int FinalScore { get; set; }
    public bool GameLost { get; set; }
    public bool CompletedAll { get; set; }
    public int CorrectCount { get; set; }
    public DateTimeOffset PlayedAt { get; set; }
}
