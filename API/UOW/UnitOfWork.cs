using System.Data;
using API.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace API.UoW
{
    public class UnitOfWork<TContext> : IUnitOfWork, IAsyncDisposable where TContext : DbContext, new()
    {
        public TContext _dbCtx = new();

        public UnitOfWork()
        {
            _dbCtx = new();
        }

        public UnitOfWork(TContext context)
        {
            _dbCtx = context;
        }

        IDbContextTransaction _transaction = null;

        private readonly Dictionary<Type, object> _repositories = new Dictionary<Type, object>();

        public IRepository<Test> TestRepo => GetRepository<Test>();

        public IRepository<Question> QuestionRepo => GetRepository<Question>();

        public IRepository<Technology> TechnologyRepo => GetRepository<Technology>();

        public IRepository<TestQuestion> TestQuestionRepo => GetRepository<TestQuestion>();

        public IRepository<User> UserRepo => GetRepository<User>();


        public async Task CommitTransactionAsync()
        {
            await _transaction.CommitAsync();
            _transaction = null;
        }

        public async Task CreateTransactionAsync(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
        {
            _transaction = await _dbCtx.Database.BeginTransactionAsync(isolationLevel);
        }

        public async Task RollbackAsync()
        {
            await _transaction.RollbackAsync();
            _transaction = null;
        }

        public async Task SaveAsync()
        {
            await _dbCtx.SaveChangesAsync();
        }

        public async ValueTask DisposeAsync()
        {
            if (_transaction != null)
            {
                await _transaction.RollbackAsync();
            }

            await _dbCtx.DisposeAsync();
        }

        private IRepository<T> GetRepository<T>() where T : class
        {
            if (_repositories.ContainsKey(typeof(T)))
            {
                return (IRepository<T>)_repositories[typeof(T)];
            }

            var repository = new Repository<T>(_dbCtx.Set<T>());
            _repositories.Add(typeof(T), repository);
            return repository;
        }
    }
}