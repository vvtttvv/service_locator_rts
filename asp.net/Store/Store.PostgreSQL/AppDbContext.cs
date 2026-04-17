using Microsoft.EntityFrameworkCore;
using Store.Domain.Entities;

namespace Store.PostgreSQL;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
	public DbSet<Category> Categories => Set<Category>();
	public DbSet<Order> Orders => Set<Order>();
	public DbSet<Product> Products => Set<Product>();

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
	}
}