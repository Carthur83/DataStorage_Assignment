using System.Linq.Expressions;

namespace Data.Interfaces;

public interface IBaseRepository<TEntity> where TEntity : class
{
    Task BeginTransactionAsync();
    Task CommitTransactionAsync();
    Task RollbackTransactionAsync();
    Task CreateAsync(TEntity entity);
    Task<IEnumerable<TEntity>> GetAllAsync();
    Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> expression);
    void Update(TEntity existingEntity, TEntity updatedEntity);
    void Delete(TEntity entity);
    Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> expression);
    Task<int> SaveAsync();
}