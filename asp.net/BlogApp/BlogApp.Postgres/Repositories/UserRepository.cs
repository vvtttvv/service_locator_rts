using BlogApp.Domain.Entities;
using BlogApp.Repositories;

namespace BlogApp.Postgres.Repositories;

public class UserRepository(AppDbContext dbContext)  : GenericRepository<User>(dbContext), IUserRepository;