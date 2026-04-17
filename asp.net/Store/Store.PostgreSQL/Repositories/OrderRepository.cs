using Microsoft.EntityFrameworkCore;
using Store.Domain.Entities;
using Store.Repositories.Interfaces;

namespace Store.PostgreSQL.Repositories;

public class OrderRepository(AppDbContext dbContext) : IOrderRepository
{
	public async Task<IReadOnlyCollection<Order>> GetAllAsync() =>
		await dbContext.Orders
			.AsNoTracking()
			.OrderBy(x => x.Id)
			.ToListAsync();

	public async Task<Order?> GetByIdAsync(int id) =>
		await dbContext.Orders
			.AsNoTracking()
			.FirstOrDefaultAsync(x => x.Id == id);

	public async Task<Order> AddAsync(Order order)
	{
		await dbContext.Orders.AddAsync(order);
		await dbContext.SaveChangesAsync();
		return order;
	}

	public async Task<Order?> UpdateAsync(Order order)
	{
		var existing = await dbContext.Orders.FirstOrDefaultAsync(x => x.Id == order.Id);
		if (existing is null)
		{
			return null;
		}

		existing.ProductId = order.ProductId;
		existing.Quantity = order.Quantity;
		await dbContext.SaveChangesAsync();
		return existing;
	}

	public async Task<bool> DeleteAsync(int id)
	{
		var existing = await dbContext.Orders.FirstOrDefaultAsync(x => x.Id == id);
		if (existing is null)
		{
			return false;
		}

		dbContext.Orders.Remove(existing);
		await dbContext.SaveChangesAsync();
		return true;
	}
}