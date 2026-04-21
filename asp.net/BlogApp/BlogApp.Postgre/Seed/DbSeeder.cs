namespace BlogApp.Postgre.Seed;

public static class DbSeeder
{
	public static async Task SeedAsync(AppDbContext dbContext, CancellationToken cancellationToken = default)
	{
		var users = await UserSeeder.SeedAsync(dbContext);
		var posts = await PostSeeder.SeedAsync(dbContext, users);
		await CommentSeeder.SeedAsync(dbContext, users, posts);
	}
}
