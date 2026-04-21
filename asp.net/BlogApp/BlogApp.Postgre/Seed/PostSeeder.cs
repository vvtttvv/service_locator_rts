using BlogApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Postgre.Seed;

public static class PostSeeder
{
	public static async Task<Post[]> SeedAsync(
		AppDbContext dbContext,
		IReadOnlyCollection<User> users,
		CancellationToken cancellationToken = default)
	{
		if (await dbContext.Posts.AnyAsync(cancellationToken))
		{
			return await dbContext.Posts
				.AsNoTracking()
				.OrderBy(x => x.Title)
				.ToArrayAsync(cancellationToken);
		}

		var admin = users.First(x => x.UserName == "admin");
		var john = users.First(x => x.UserName == "john");
		var jane = users.First(x => x.UserName == "jane");

		var posts = new[]
		{
			new Post { Title = "Welcome to BlogApp", Description = "First post from admin.", UserId = admin.Id },
			new Post { Title = "ASP.NET Tips", Description = "A few practical ASP.NET tips.", UserId = john.Id },
			new Post { Title = "Entity Framework Basics", Description = "How to start with EF Core.", UserId = jane.Id }
		};

		await dbContext.Posts.AddRangeAsync(posts, cancellationToken);
		await dbContext.SaveChangesAsync(cancellationToken);
		return posts;
	}
}
