
using Microsoft.EntityFrameworkCore;

namespace API.UoW
{
    public interface IRepository<T> where T : class
    {
        DbSet<T> All { get; }

        IQueryable<T> GetAll();

        void Insert(T obj);

        void Delete(T obj);
    }
}