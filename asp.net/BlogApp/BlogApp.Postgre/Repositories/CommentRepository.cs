using BlogApp.Domain.Entities;
using BlogApp.Repositories;

namespace BlogApp.Postgre.Repositories;

public class CommentRepository(AppDbContext dbContext)  : GenericRepository<Comment>(dbContext), ICommentRepository;