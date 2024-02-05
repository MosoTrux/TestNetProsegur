using System.Linq.Expressions;
using TestNetProsegur.Core.Entities;

namespace TestNetProsegur.Core.Repositories
{
    public interface IRepository<T> where T : BaseEntity
    {
        IQueryable<T> GetAll();
        Task<T> GetById(long id);
        Task Add(T entity);
        void Update(T entity);
        void UpdateRange(IEnumerable<T> entities);
        Task Delete(long id);
        IQueryable<T> GetBy(Expression<Func<T, bool>> predicate);
        void AddRange(IEnumerable<T> entities);
        void RemoveRange(IEnumerable<T> entities);
        Task SaveChangesAsync();
    }
}



