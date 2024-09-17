using API.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace API.Database
{
    public class TestDbContext : IdentityDbContext<User>
    {
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

            // builder.Entity<User>()
            //     .HasMany(e => e.Tests)
            //     .WithOne(e => e.User)
            //     .HasForeignKey(e => e.UserId)
            //     .HasPrincipalKey(e => e.Id)
            //     .OnDelete(DeleteBehavior.Restrict);

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
        }

        public DbSet<Question> Questions { get; set; }

        public DbSet<Technology> Technologies { get; set; }

        public DbSet<Test> Tests { get; set; }

        public DbSet<TestQuestion> TestQuestions { get; set; }
    }
}