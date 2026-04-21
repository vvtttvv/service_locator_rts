using BlogApp.Postgre;
using BlogApp.Postgre.Seed;
using BlogApp.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddServicesLayer();
builder.Services.AddDatabaseLayer(builder.Configuration);

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
	var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
	await dbContext.Database.MigrateAsync();
	await DbSeeder.SeedAsync(dbContext);
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
