using Microsoft.EntityFrameworkCore;
using quizApi.Models;
using quizApi.Services;

namespace quizApi.Data;

public static class DbInitializer
{
    public static async Task InitializeAsync(QuizDbContext db, CancellationToken ct = default)
    {
        await db.Database.EnsureCreatedAsync(ct);
        await EnsureQuizTablesAsync(db, ct);

        if (!await db.Quizzes.AnyAsync(ct))
        {
            var quizzes = new List<QuizRow>
            {
                new()
                {
                    CategorySlug = "film-dizi",
                    Title = "Film & Dizi",
                    Description = "Kolay seviye sorular."
                },
                new()
                {
                    CategorySlug = "spor",
                    Title = "Spor",
                    Description = "Kolay seviye sorular."
                },
                new()
                {
                    CategorySlug = "cografya",
                    Title = "Coğrafya",
                    Description = "Kolay seviye sorular."
                },
                new()
                {
                    CategorySlug = "genel-kultur",
                    Title = "Genel Kültür",
                    Description = "Kolay seviye sorular."
                },
                new()
                {
                    CategorySlug = "bilmece",
                    Title = "Bilmece",
                    Description = "Otuz bilmece havuzundan her turda yirmi soru."
                }
            };

            db.Quizzes.AddRange(quizzes);
            await db.SaveChangesAsync(ct);

            var ordered = await db.Quizzes.OrderBy(q => q.Id).ToListAsync(ct);
            foreach (var quiz in ordered)
                AddQuestionRows(db, quiz.Id, SeedForSlug(quiz.CategorySlug));

            await db.SaveChangesAsync(ct);
        }

        await UpgradePlaceholderCategoriesAsync(db, ct);
        await SyncMissingSeedQuestionsAsync(db, ct);
    }

    private static IReadOnlyList<Question> SeedForSlug(string slug) =>
        slug switch
        {
            "film-dizi" => CategoryEasyQuestionsSeed.FilmDizi.Concat(PoolExtraSeed.FilmDizi).ToList(),
            "spor" => CategoryEasyQuestionsSeed.Spor.Concat(PoolExtraSeed.Spor).ToList(),
            "cografya" => CategoryEasyQuestionsSeed.Cografya.Concat(PoolExtraSeed.Cografya).ToList(),
            "genel-kultur" => CategoryEasyQuestionsSeed.GenelKultur.Concat(PoolExtraSeed.GenelKultur).ToList(),
            "bilmece" => BilmeceQuestionsSeed.All.Concat(PoolExtraSeed.Bilmece).ToList(),
            _ => Array.Empty<Question>()
        };

    private static async Task SyncMissingSeedQuestionsAsync(QuizDbContext db, CancellationToken ct)
    {
        foreach (var slug in new[] { "film-dizi", "spor", "cografya", "genel-kultur", "bilmece" })
        {
            var quiz = await db.Quizzes.FirstOrDefaultAsync(q => q.CategorySlug == slug, ct);
            if (quiz is null) continue;

            var existing = await db.Questions.Where(q => q.QuizId == quiz.Id).Select(q => q.Text).ToListAsync(ct);
            var set = new HashSet<string>(existing, StringComparer.OrdinalIgnoreCase);
            var seed = SeedForSlug(slug);
            var hasQuestions = await db.Questions.AnyAsync(q => q.QuizId == quiz.Id, ct);
            var order = hasQuestions
                ? await db.Questions.Where(q => q.QuizId == quiz.Id).MaxAsync(q => q.SortOrder, ct) + 1
                : 0;

            foreach (var q in seed)
            {
                if (set.Contains(q.Text)) continue;
                var opts = q.Options.ToList();
                db.Questions.Add(new QuestionRow
                {
                    QuizId = quiz.Id,
                    Text = q.Text,
                    Option0 = opts[0],
                    Option1 = opts[1],
                    Option2 = opts[2],
                    Option3 = opts[3],
                    CorrectOptionIndex = q.CorrectOptionIndex,
                    SortOrder = order++
                });
                set.Add(q.Text);
            }

            if (slug == "bilmece")
                quiz.Description = "Otuz bilmece havuzundan her turda yirmi soru.";
        }

        await db.SaveChangesAsync(ct);
    }

    private static void AddQuestionRows(QuizDbContext db, int quizId, IReadOnlyList<Question> seed)
    {
        var order = 0;
        foreach (var q in seed)
        {
            var opts = q.Options.ToList();
            db.Questions.Add(new QuestionRow
            {
                QuizId = quizId,
                Text = q.Text,
                Option0 = opts[0],
                Option1 = opts[1],
                Option2 = opts[2],
                Option3 = opts[3],
                CorrectOptionIndex = q.CorrectOptionIndex,
                SortOrder = order++
            });
        }
    }

    /// <summary>
    /// Eski veritabanlarında tek geçici soru kaldıysa kolay soru setiyle değiştirir.
    /// </summary>
    private static async Task UpgradePlaceholderCategoriesAsync(QuizDbContext db, CancellationToken ct)
    {
        var slugs = new[] { "film-dizi", "spor", "cografya", "genel-kultur" };

        foreach (var slug in slugs)
        {
            var quiz = await db.Quizzes.FirstOrDefaultAsync(q => q.CategorySlug == slug, ct);
            if (quiz is null) continue;

            var qs = await db.Questions.Where(q => q.QuizId == quiz.Id).ToListAsync(ct);
            var onlyPlaceholder = qs.Count == 1 && CategoryEasyQuestionsSeed.IsPlaceholderQuestion(qs[0].Text);
            if (qs.Count != 0 && !onlyPlaceholder) continue;

            db.Questions.RemoveRange(qs);
            await db.SaveChangesAsync(ct);

            AddQuestionRows(db, quiz.Id, SeedForSlug(slug));
            await db.SaveChangesAsync(ct);

            quiz.Description = "Kolay seviye sorular.";
        }

        await db.SaveChangesAsync(ct);
    }

    private static async Task EnsureQuizTablesAsync(QuizDbContext db, CancellationToken ct)
    {
        await db.Database.ExecuteSqlRawAsync(
            """
            CREATE TABLE IF NOT EXISTS "Quizzes" (
                "Id" INTEGER NOT NULL CONSTRAINT "PK_Quizzes" PRIMARY KEY AUTOINCREMENT,
                "CategorySlug" TEXT NOT NULL,
                "Title" TEXT NOT NULL,
                "Description" TEXT NULL
            );
            """,
            cancellationToken: ct);

        await db.Database.ExecuteSqlRawAsync(
            """
            CREATE TABLE IF NOT EXISTS "Questions" (
                "Id" INTEGER NOT NULL CONSTRAINT "PK_Questions" PRIMARY KEY AUTOINCREMENT,
                "QuizId" INTEGER NOT NULL,
                "Text" TEXT NOT NULL,
                "Option0" TEXT NOT NULL,
                "Option1" TEXT NOT NULL,
                "Option2" TEXT NOT NULL,
                "Option3" TEXT NOT NULL,
                "CorrectOptionIndex" INTEGER NOT NULL,
                "SortOrder" INTEGER NOT NULL,
                CONSTRAINT "FK_Questions_Quizzes_QuizId" FOREIGN KEY ("QuizId") REFERENCES "Quizzes" ("Id") ON DELETE CASCADE
            );
            """,
            cancellationToken: ct);

        await db.Database.ExecuteSqlRawAsync(
            """
            CREATE INDEX IF NOT EXISTS "IX_Questions_QuizId" ON "Questions" ("QuizId");
            """,
            cancellationToken: ct);

        await db.Database.ExecuteSqlRawAsync(
            """
            CREATE UNIQUE INDEX IF NOT EXISTS "IX_Quizzes_CategorySlug" ON "Quizzes" ("CategorySlug");
            """,
            cancellationToken: ct);
    }
}
