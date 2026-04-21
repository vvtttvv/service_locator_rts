using BlogApp.Domain.Entities;

namespace BlogApp.Services.Interfaces;

public interface IPostService
{
	Task<IReadOnlyCollection<Post>> GetAllAsync();
	Task<Post?> GetByIdAsync(Guid id);
	Task<Post> CreateAsync(Post post);
	Task<Post> UpdateAsync(Guid id, Post post);
	Task DeleteAsync(Guid id);
}