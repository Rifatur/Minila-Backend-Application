using Microsoft.EntityFrameworkCore;
using RidingApp.Core.Services.Interfaces;
using RidingApp.DataAccess;

namespace RidingApp.Core.Services.Implementations
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly ApplicationDbContext _context;

        public Repository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<T> AddAsync(T entity, CancellationToken cancellationToken = default)
        {
            await _context.Set<T>().AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync();
            return entity;
        }

        public Task<List<T>> AddRangeAsync(T[] entities, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(T entity, CancellationToken cancellationToken = default)
        {
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();

        }

        public Task DeleteRangeAsync(T[] entities, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<IQueryable<T>> GetAsQueryableAsync(Func<IQueryable<T>, IQueryable<T>> expression = null, CancellationToken cancellationToken = default)
        {
            IQueryable<T> query = _context.Set<T>();

            if (expression != null) query = expression(query);

            return Task.FromResult(query.AsQueryable());
        }

        public async Task<T> GetByIdAsync(int id)
        {
            var result = await _context.Set<T>().FindAsync(id);
            return result;
        }

        public async Task<T> GetByIdLongAsync(long id)
        {
            var res = await _context.Set<T>().FindAsync(id);
            return res;
        }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<T> UpdateAsync(T entity, CancellationToken cancellationToken = default)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync(cancellationToken);
            return entity;
        }
    }
}
