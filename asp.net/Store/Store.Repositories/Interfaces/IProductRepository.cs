using Store.Domain.Entities;

namespace Store.Repositories.Interfaces;

public interface IProductRepository
{
	Task<IReadOnlyCollection<Product>> GetAllAsync();
	Task<Product?> GetByIdAsync(int id);
	Task<Product> AddAsync(Product product);
	Task<Product?> UpdateAsync(Product product);
	Task<bool> DeleteAsync(int id);
}