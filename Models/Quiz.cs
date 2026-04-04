namespace quizApi.Models;

public class Quiz
{
    public int Id { get; set; }
    public string CategorySlug { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public IReadOnlyList<Question> Questions { get; set; } = Array.Empty<Question>();
}
