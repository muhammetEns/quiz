using Microsoft.EntityFrameworkCore;

namespace quizApi.Data;

public class QuizDbContext : DbContext
{
    public QuizDbContext(DbContextOptions<QuizDbContext> options)
        : base(options)
    {
    }

    public DbSet<GameScore> GameScores => Set<GameScore>();
    public DbSet<QuizRow> Quizzes => Set<QuizRow>();
    public DbSet<QuestionRow> Questions => Set<QuestionRow>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<GameScore>(e =>
        {
            e.HasKey(x => x.Id);
            e.Property(x => x.PlayerName).HasMaxLength(80).IsRequired();
            e.HasIndex(x => new { x.QuizId, x.CompletedAll, x.GameLost, x.FinalScore });
        });

        modelBuilder.Entity<QuizRow>(e =>
        {
            e.HasKey(x => x.Id);
            e.Property(x => x.CategorySlug).HasMaxLength(64).IsRequired();
            e.Property(x => x.Title).HasMaxLength(200).IsRequired();
            e.HasIndex(x => x.CategorySlug).IsUnique();
            e.HasMany(x => x.Questions)
                .WithOne(x => x.Quiz!)
                .HasForeignKey(x => x.QuizId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<QuestionRow>(e =>
        {
            e.HasKey(x => x.Id);
            e.Property(x => x.Text).HasMaxLength(2000).IsRequired();
            e.Property(x => x.Option0).HasMaxLength(500).IsRequired();
            e.Property(x => x.Option1).HasMaxLength(500).IsRequired();
            e.Property(x => x.Option2).HasMaxLength(500).IsRequired();
            e.Property(x => x.Option3).HasMaxLength(500).IsRequired();
            e.HasIndex(x => new { x.QuizId, x.SortOrder });
        });
    }
}
