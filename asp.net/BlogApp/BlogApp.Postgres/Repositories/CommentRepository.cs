using BlogApp.Domain.Entities;
using BlogApp.Repositories;

namespace BlogApp.Postgres.Repositories;

public class CommentRepository(AppDbContext dbContext)  : GenericRepository<Comment>(dbContext), ICommentRepository;