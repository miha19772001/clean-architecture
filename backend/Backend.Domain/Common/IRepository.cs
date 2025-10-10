namespace Backend.Domain.Common
{
    public interface IRepository<TEntity> where TEntity : AggregateRoot
    {
        IQueryable<TEntity> CreateQuery();

        Task<List<TEntity>> GetAllAsync(CancellationToken cancellationToken = default);

        Task<TEntity> GetByIdAsync(int id, CancellationToken cancellationToken = default);

        Task CreateAsync(TEntity entity, CancellationToken cancellationToken = default);

        Task RemoveAsync(TEntity entity, CancellationToken cancellationToken = default);

        Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);
    }
}
