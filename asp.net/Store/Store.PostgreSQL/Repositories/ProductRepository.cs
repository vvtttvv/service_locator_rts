using Microsoft.EntityFrameworkCore;
using Store.Domain.Entities;
using Store.Repositories.Interfaces;

namespace Store.PostgreSQL.Repositories;

public class ProductRepository(AppDbContext dbContext) : IProductRepository
{
	public async Task<IReadOnlyCollection<Product>> GetAllAsync() =>
		await dbContext.Products
			.AsNoTracking()
			.OrderBy(x => x.Id)
			.ToListAsync();

	public async Task<Product?> GetByIdAsync(int id) =>
		await dbContext.Products
			.AsNoTracking()
			.FirstOrDefaultAsync(x => x.Id == id);

	public async Task<Product> AddAsync(Product product)
	{
		await dbContext.Products.AddAsync(product);
		await dbContext.SaveChangesAsync();
		return product;
	}

	public async Task<Product?> UpdateAsync(Product product)
	{
		var existing = await dbContext.Products.FirstOrDefaultAsync(x => x.Id == product.Id);
		if (existing is null)
		{
			return null;
		}

		existing.Name = product.Name;
		existing.Price = product.Price;
		existing.CategoryId = product.CategoryId;
		await dbContext.SaveChangesAsync();
		return existing;
	}

	public async Task<bool> DeleteAsync(int id)
	{
		var existing = await dbContext.Products.FirstOrDefaultAsync(x => x.Id == id);
		if (existing is null)
		{
			return false;
		}

		dbContext.Products.Remove(existing);
		await dbContext.SaveChangesAsync();
		return true;
	}
}