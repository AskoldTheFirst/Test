using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace API.Database
{
    public class TestDbContext : IdentityDbContext<User>
    {
        public TestDbContext()
        {
        }

        public TestDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Test>()
                .HasMany(e => e.TestQuestions)
                .WithOne(e => e.Test)
                .HasForeignKey(e => e.TestId)
                .HasPrincipalKey(e => e.Id)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Question>()
                .HasMany(e => e.TestQuestions)
                .WithOne(e => e.Question)
                .HasForeignKey(e => e.QuestionId)
                .HasPrincipalKey(e => e.Id)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Technology>()
                .HasMany(e => e.Questions)
                .WithOne(e => e.Technology)
                .HasForeignKey(e => e.TechnologyId)
                .HasPrincipalKey(e => e.Id)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Technology>()
                .HasMany(e => e.Tests)
                .WithOne(e => e.Technology)
                .HasForeignKey(e => e.TechnologyId)
                .HasPrincipalKey(e => e.Id)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<IdentityRole>()
                .HasData(
                    new IdentityRole { Name = "Member", NormalizedName = "MEMBER" },
                    new IdentityRole { Name = "Admin", NormalizedName = "ADMIN" }
                );

            builder.Entity<Test>().HasIndex(e => e.Username);
            builder.Entity<Test>().HasIndex(e => e.FinalScore);

            builder.Entity<Message>().Property(c => c.Data).HasDefaultValueSql("getdate()");
            builder.Entity<Message>().HasIndex(c => c.Username);
        }

        public DbSet<Question> Questions { get; set; }

        public DbSet<Technology> Technologies { get; set; }

        public DbSet<Test> Tests { get; set; }

        public DbSet<TestQuestion> TestQuestions { get; set; }

        public DbSet<Message> Messages { get; set; }
    }
}