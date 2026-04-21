using BlogApp.Domain.Entities;
using BlogApp.Repositories;

namespace BlogApp.Postgre.Repositories;

public class PostRepository(AppDbContext dbContext)  : GenericRepository<Post>(dbContext), IPostRepository;