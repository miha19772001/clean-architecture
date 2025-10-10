namespace Backend.Domain.Common
{
    public interface IIncludeDetails<TEntity> where TEntity : Entity
    {
        IQueryable<TEntity> ApplyIncludes(IQueryable<TEntity> query);
    }
}
