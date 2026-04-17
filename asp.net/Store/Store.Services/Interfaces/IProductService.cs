using Store.Domain.Entities;

namespace Store.Services.Interfaces;

public interface IProductService
{
	Task<IReadOnlyCollection<Product>> GetAllAsync();
	Task<Product?> GetByIdAsync(int id);
	Task<Product> CreateAsync(Product product);
	Task<Product> UpdateAsync(int id, Product product);
	Task DeleteAsync(int id);
}