using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TestNetProsegur.Core.Entities;
using TestNetProsegur.Core.Repositories;
using TestNetProsegur.Infrastructure.DBContexts;

namespace TestNetProsegur.Infrastructure.Repositories
{
    public class BaseRepository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly AppDbContext _context;
        protected readonly DbSet<T> _entities;

        public BaseRepository(AppDbContext context)
        {
            _context = context;
            _entities = context.Set<T>();
        }

        public IQueryable<T> GetAll()
        {
            return _entities;
        }

        public async Task<T> GetById(long id)
        {
            return await _entities.FindAsync(id).ConfigureAwait(false);
        }

        public IQueryable<T> GetBy(Expression<Func<T, bool>> predicate)
        {
            return _entities.Where(predicate);
        }

        public async Task Add(T entity)
        {
            await _entities.AddAsync(entity);
        }

        public void Update(T entity)
        {
            _entities.Update(entity);
        }

        public void UpdateRange(IEnumerable<T> entities)
        {
            _entities.UpdateRange(entities);
        }

        public async Task Delete(long id)
        {
            T entity = await GetById(id);
            _entities.Remove(entity);
        }

        public void AddRange(IEnumerable<T> entities)
        {
            _entities.AddRange(entities);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            _entities.RemoveRange(entities);
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

    }
}
