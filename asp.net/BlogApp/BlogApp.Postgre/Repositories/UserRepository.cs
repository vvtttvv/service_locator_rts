using BlogApp.Domain.Entities;
using BlogApp.Repositories;

namespace BlogApp.Postgre.Repositories;

public class UserRepository(AppDbContext dbContext)  : GenericRepository<User>(dbContext), IUserRepository;