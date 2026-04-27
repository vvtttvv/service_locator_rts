using Microsoft.EntityFrameworkCore;
using BlogApp.Domain.Entities;
using BlogApp.Repositories;
using System.Linq.Expressions;

namespace BlogApp.Postgres.Repositories;

public abstract class GenericRepository<TEntity>(AppDbContext dbContext) : IGenericRepository<TEntity>
	where TEntity : BaseEntity
{
	protected readonly AppDbContext DbContext = dbContext;

	public virtual async Task<IReadOnlyCollection<TEntity>> GetAllAsync(int startIndex, int maxCount, 
		List<Expression<Func<TEntity, object>>> includes = null!, bool asNoTracking = true )
	{
		var query = BuildQuery( startIndex, maxCount, asNoTracking, includes);
		return await query.ToListAsync();
	}

	public virtual async Task<TEntity?> GetByIdAsync(Guid id)
	{
		return await DbContext.Set<TEntity>()
			.AsNoTracking()
			.FirstOrDefaultAsync(x => x.Id == id);
	}

	public virtual async Task<TEntity> AddAsync(TEntity entity)
	{
		await DbContext.Set<TEntity>().AddAsync(entity);
		await SaveChangesWithAuditAsync();
		return entity;
	}

	public virtual async Task<TEntity?> UpdateByIdAsync(Guid id, TEntity entity)
	{
		var existing = await DbContext.Set<TEntity>().FirstOrDefaultAsync(x => x.Id == id);
		if (existing is null)
		{
			return null;
		}

		DbContext.Entry(existing).CurrentValues.SetValues(entity);
		existing.Id = id;
		await SaveChangesWithAuditAsync();
		return existing;
	}

	public virtual async Task<bool> DeleteAsync(Guid id)
	{
		var existing = await DbContext.Set<TEntity>().FirstOrDefaultAsync(x => x.Id == id);
		if (existing is null)
		{
			return false;
		}

		DbContext.Set<TEntity>().Remove(existing);
		await SaveChangesWithAuditAsync();
		return true;
	}

	protected IQueryable<TEntity> BuildQuery(int startIndex, int maxCount, bool asNoTracking = true, 
				List<Expression<Func<TEntity, object>>> includes = null! )
	{
		IQueryable<TEntity> query = DbContext.Set<TEntity>();
		if (asNoTracking)
		{
			query = query.AsNoTracking();
		}
		
		foreach (var include in includes)
		{
			query = query.Include(include);
		}


		if (startIndex > 0)
		{
			query = query.Skip(startIndex);
		}

		if (maxCount > 0)
		{
			query = query.Take(maxCount);
		}

		return query;
	}

	private async Task<int> SaveChangesWithAuditAsync(CancellationToken cancellationToken = default)
	{
		foreach (var entry in DbContext.ChangeTracker.Entries<BaseEntity>())
		{
			if (entry.State == EntityState.Modified)
			{
				entry.Entity.UpdatedAt = DateTime.UtcNow;
			}
		}

		return await DbContext.SaveChangesAsync(cancellationToken);
	}
}
