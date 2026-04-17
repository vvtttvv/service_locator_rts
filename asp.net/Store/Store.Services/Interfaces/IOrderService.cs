using Store.Domain.Entities;

namespace Store.Services.Interfaces;

public interface IOrderService
{
	Task<IReadOnlyCollection<Order>> GetAllAsync();
	Task<Order?> GetByIdAsync(int id);
	Task<Order> CreateAsync(Order order);
	Task<Order> UpdateAsync(int id, Order order);
	Task DeleteAsync(int id);
}