using Microsoft.EntityFrameworkCore;

namespace API.UnitOfWork
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly DbSet<T> _dbSet;

        public DbSet<T> All { get { return _dbSet; } }

        public Repository(DbSet<T> dbSet)
        {
            _dbSet = dbSet;
        }

        public IQueryable<T> GetAll()
        {
            return _dbSet.AsQueryable<T>();
        }

        public void Insert(T entity)
        {
            _dbSet.Add(entity);
        }

        public void InsertRange(T[] objArray)
        {
            _dbSet.AddRange(objArray);
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }
    }
}