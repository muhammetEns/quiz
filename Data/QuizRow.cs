namespace quizApi.Data;

public class QuizRow
{
    public int Id { get; set; }
    public string CategorySlug { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public ICollection<QuestionRow> Questions { get; set; } = new List<QuestionRow>();
}
