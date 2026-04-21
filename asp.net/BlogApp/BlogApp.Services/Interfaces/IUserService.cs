using BlogApp.Domain.Entities;

namespace BlogApp.Services.Interfaces;

public interface IUserService
{
	Task<IReadOnlyCollection<User>> GetAllAsync();
	Task<User?> GetByIdAsync(Guid id);
	Task<User> CreateAsync(User user);
	Task<User> UpdateAsync(Guid id, User user);
	Task DeleteAsync(Guid id);
}