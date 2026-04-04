namespace quizApi.Data;

public class QuestionRow
{
    public int Id { get; set; }
    public int QuizId { get; set; }
    public string Text { get; set; } = string.Empty;
    public string Option0 { get; set; } = string.Empty;
    public string Option1 { get; set; } = string.Empty;
    public string Option2 { get; set; } = string.Empty;
    public string Option3 { get; set; } = string.Empty;
    public int CorrectOptionIndex { get; set; }
    public int SortOrder { get; set; }
    public QuizRow? Quiz { get; set; }

    public IReadOnlyList<string> OptionsList() => [Option0, Option1, Option2, Option3];
}
