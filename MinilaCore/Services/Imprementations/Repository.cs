using Microsoft.EntityFrameworkCore;
using MinilaCore.Services.Interfaces;
using MinilaDataAcess.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinilaCore.Services.Imprementations
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly MinilaDBContext _dbContext;

        public Repository(MinilaDBContext dbcontext)
        {
            _dbContext = dbcontext;
        }
        public async Task<T> AddAsync(T entity, CancellationToken cancellationToken = default)
        {
            await _dbContext.Set<T>().AddAsync(entity, cancellationToken);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public Task<List<T>> AddRangeAsync(T[] entities, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(T entity, CancellationToken cancellationToken = default)
        {
            _dbContext.Set<T>().Remove(entity);
        }

        public Task DeleteRangeAsync(T[] entities, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<IQueryable<T>> GetAsQueryableAsync(Func<IQueryable<T>, IQueryable<T>> expression = null, CancellationToken cancellationToken = default)
        {
            IQueryable<T> query = _dbContext.Set<T>();

            if (expression != null) query = expression(query);

            return Task.FromResult(query.AsQueryable());
        }
        public async Task<T> GetByIdAsync(long id)
        {

            var res = await _dbContext.Set<T>().FindAsync(id);
            return res;
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<T> UpdateAsync(T entity, CancellationToken cancellationToken = default)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync(cancellationToken);
            return entity;
        }





    }
}
