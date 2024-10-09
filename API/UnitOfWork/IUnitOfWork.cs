using System.Data;
using Microsoft.EntityFrameworkCore;
using API.Database.Entities;

namespace API.UnitOfWork
{
    public interface IUnitOfWork
    {
        IRepository<Test> TestRepo { get; }

        IRepository<Question> QuestionRepo { get; }

        IRepository<Technology> TechnologyRepo { get; }

        IRepository<TestQuestion> TestQuestionRepo { get; }

        IRepository<Message> MessageRepo { get; }

        IRepository<User> UserRepo { get; }

        Task CreateTransactionAsync(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted);

        Task CommitTransactionAsync();

        Task RollbackAsync();
        
        Task SaveAsync();
    }
}