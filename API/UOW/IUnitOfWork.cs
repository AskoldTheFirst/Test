using Microsoft.EntityFrameworkCore;
using API.Database.Entities;

namespace API.UoW
{
    public interface IUnitOfWork
    {
        IRepository<Test> TestRepo { get; }

        IRepository<Question> QuestionRepo { get; }

        IRepository<Technology> TechnologyRepo { get; }

        IRepository<TestQuestion> TestQuestionRepo { get; }

        IRepository<User> UserRepo { get; }

        Task CreateTransactionAsync();

        Task CommitTransactionAsync();

        Task RollbackAsync();
        
        Task SaveAsync();
    }
}