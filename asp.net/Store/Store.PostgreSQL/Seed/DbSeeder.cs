using Microsoft.EntityFrameworkCore;
using Store.Domain.Entities;

namespace Store.PostgreSQL.Seed;

public static class DbSeeder
{
	public static async Task SeedAsync(AppDbContext dbContext, CancellationToken cancellationToken = default)
	{
		if (await dbContext.Categories.AnyAsync(cancellationToken))
		{
			return;
		}

		var categories = new[]
		{
			new Category { Name = "Electronics" },
			new Category { Name = "Books" },
			new Category { Name = "Home" }
		};

		await dbContext.Categories.AddRangeAsync(categories, cancellationToken);
		await dbContext.SaveChangesAsync(cancellationToken);

		var products = new[]
		{
			new Product { Name = "Laptop", Price = 1299.99m, CategoryId = categories[0].Id },
			new Product { Name = "Clean Code", Price = 39.90m, CategoryId = categories[1].Id },
			new Product { Name = "Desk Lamp", Price = 25.50m, CategoryId = categories[2].Id }
		};

		await dbContext.Products.AddRangeAsync(products, cancellationToken);
		await dbContext.SaveChangesAsync(cancellationToken);

		var orders = new[]
		{
			new Order { ProductId = products[0].Id, Quantity = 1 },
			new Order { ProductId = products[1].Id, Quantity = 2 }
		};

		await dbContext.Orders.AddRangeAsync(orders, cancellationToken);
		await dbContext.SaveChangesAsync(cancellationToken);
	}
}