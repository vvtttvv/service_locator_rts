using System.Linq.Expressions;
using BlogApp.Domain.Entities;

namespace BlogApp.Repositories;

public interface IGenericRepository<TEntity> where TEntity : BaseEntity
{
    Task<IReadOnlyCollection<TEntity>> GetAllAsync(int startIndex, int maxCount, 
            List<Expression<Func<TEntity, object>>> includes = null!, bool asNoTracking = true );
    Task<TEntity?> GetByIdAsync(Guid id);
    Task<TEntity> AddAsync(TEntity entity);
    Task<TEntity?> UpdateByIdAsync(Guid id, TEntity entity);
    Task<bool> DeleteAsync(Guid id);
}
