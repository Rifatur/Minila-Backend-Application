namespace RidingApp.Core.Services.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<T> GetByIdLongAsync(long id);
        Task<T> GetByIdAsync(int id);
        Task<IQueryable<T>> GetAsQueryableAsync(Func<IQueryable<T>, IQueryable<T>> expression = null, CancellationToken cancellationToken = default);
        Task<T> AddAsync(T entity, CancellationToken cancellationToken = default);
        Task<List<T>> AddRangeAsync(T[] entities, CancellationToken cancellationToken = default);
        Task<T> UpdateAsync(T entity, CancellationToken cancellationToken = default);
        Task DeleteAsync(T entity, CancellationToken cancellationToken = default);
        Task DeleteRangeAsync(T[] entities, CancellationToken cancellationToken = default);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
