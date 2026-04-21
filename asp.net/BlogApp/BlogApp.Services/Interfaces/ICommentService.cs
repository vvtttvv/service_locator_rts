using BlogApp.Domain.Entities;

namespace BlogApp.Services.Interfaces;

public interface ICommentService
{
	Task<IReadOnlyCollection<Comment>> GetAllAsync();
	Task<Comment?> GetByIdAsync(Guid id);
	Task<Comment> CreateAsync(Comment comment);
	Task<Comment> UpdateAsync(Guid id, Comment comment);
	Task DeleteAsync(Guid id);
}