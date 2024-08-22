using API.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Database
{
    public class TestDbContext : DbContext
    {
        public TestDbContext(DbContextOptions options) : base (options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Test>()
                .HasMany(e => e.TestQuestions)
                .WithOne(e => e.Test)
                .HasForeignKey(e => e.TestId)
                .HasPrincipalKey(e => e.Id)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Question>()
                .HasMany(e => e.TestQuestions)
                .WithOne(e => e.Question)
                .HasForeignKey(e => e.QuestionId)
                .HasPrincipalKey(e => e.Id)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Technology>()
                .HasMany(e => e.Questions)
                .WithOne(e => e.Technology)
                .HasForeignKey(e => e.TechnologyId)
                .HasPrincipalKey(e => e.Id)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Tests)
                .WithOne(e => e.User)
                .HasForeignKey(e => e.UserId)
                .HasPrincipalKey(e => e.Id)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Technology>()
                .HasMany(e => e.Tests)
                .WithOne(e => e.Technology)
                .HasForeignKey(e => e.TechnologyId)
                .HasPrincipalKey(e => e.Id)
                .OnDelete(DeleteBehavior.Restrict);
        }

        public DbSet<Question> Questions { get; set; }

        public DbSet<Technology> Technologies { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Test> Tests { get; set; }

        public DbSet<TestQuestion> TestQuestions { get; set; }
    }
}