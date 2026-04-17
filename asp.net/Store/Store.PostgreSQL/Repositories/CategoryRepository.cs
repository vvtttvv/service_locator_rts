using Microsoft.EntityFrameworkCore;
using Store.Domain.Entities;
using Store.Repositories.Interfaces;

namespace Store.PostgreSQL.Repositories;

public class CategoryRepository(AppDbContext dbContext) : ICategoryRepository
{
	public async Task<IReadOnlyCollection<Category>> GetAllAsync() =>
		await dbContext.Categories
			.AsNoTracking()
			.OrderBy(x => x.Id)
			.ToListAsync();

	public async Task<Category?> GetByIdAsync(int id) =>
		await dbContext.Categories
			.AsNoTracking()
			.FirstOrDefaultAsync(x => x.Id == id);

	public async Task<Category> AddAsync(Category category)
	{
		await dbContext.Categories.AddAsync(category);
		await dbContext.SaveChangesAsync();
		return category;
	}

	public async Task<Category?> UpdateAsync(Category category)
	{
		var existing = await dbContext.Categories.FirstOrDefaultAsync(x => x.Id == category.Id);
		if (existing is null)
		{
			return null;
		}

		existing.Name = category.Name;
		await dbContext.SaveChangesAsync();
		return existing;
	}

	public async Task<bool> DeleteAsync(int id)
	{
		var existing = await dbContext.Categories.FirstOrDefaultAsync(x => x.Id == id);
		if (existing is null)
		{
			return false;
		}

		dbContext.Categories.Remove(existing);
		await dbContext.SaveChangesAsync();
		return true;
	}
}