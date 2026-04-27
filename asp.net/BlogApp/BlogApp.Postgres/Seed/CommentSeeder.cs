using BlogApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Postgres.Seed;

public static class CommentSeeder
{
	public static async Task<Comment[]> SeedAsync(
		AppDbContext dbContext,
		IReadOnlyCollection<User> users,
		IReadOnlyCollection<Post> posts,
		CancellationToken cancellationToken = default)
	{
		if (await dbContext.Comments.AnyAsync(cancellationToken))
		{
			return await dbContext.Comments
				.AsNoTracking()
				.ToArrayAsync(cancellationToken);
		}

		var admin = users.First(x => x.UserName == "admin");
		var john = users.First(x => x.UserName == "john");
		var jane = users.First(x => x.UserName == "jane");

		var welcomePost = posts.First(x => x.Title == "Welcome to BlogApp");
		var aspNetPost = posts.First(x => x.Title == "ASP.NET Tips");

		var rootComments = new[]
		{
			new Comment { Description = "Great start!", UserId = john.Id, PostId = welcomePost.Id },
			new Comment { Description = "Thanks for the tips.", UserId = jane.Id, PostId = aspNetPost.Id }
		};

		await dbContext.Comments.AddRangeAsync(rootComments, cancellationToken);
		await dbContext.SaveChangesAsync(cancellationToken);

		var reply = new Comment
		{
			Description = "Glad it helps.",
			UserId = admin.Id,
			PostId = aspNetPost.Id,
			ParentId = rootComments[1].Id
		};

		await dbContext.Comments.AddAsync(reply, cancellationToken);
		await dbContext.SaveChangesAsync(cancellationToken);

		return new[] { rootComments[0], rootComments[1], reply };
	}
}
