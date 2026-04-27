using BlogApp.Domain.Entities;
using BlogApp.Repositories;

namespace BlogApp.Postgres.Repositories;

public class PostRepository(AppDbContext dbContext)  : GenericRepository<Post>(dbContext), IPostRepository;