using BlogApp.Domain.Entities;
using BlogApp.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Postgre.Seed;

public static class UserSeeder
{
	public static async Task<User[]> SeedAsync(AppDbContext dbContext, CancellationToken cancellationToken = default)
	{
		if (await dbContext.Users.AnyAsync(cancellationToken))
		{
			return await dbContext.Users
				.AsNoTracking()
				.OrderBy(x => x.UserName)
				.ToArrayAsync(cancellationToken);
		}

		var users = new[]
		{
			new User { UserName = "admin", FullName = "Blog Admin", Age = 30, Role = UserType.Admin },
			new User { UserName = "john", FullName = "John Doe", Age = 24, Role = UserType.Default },
			new User { UserName = "jane", FullName = "Jane Smith", Age = 27, Role = UserType.Vip }
		};

		await dbContext.Users.AddRangeAsync(users, cancellationToken);
		await dbContext.SaveChangesAsync(cancellationToken);
		return users;
	}
}
