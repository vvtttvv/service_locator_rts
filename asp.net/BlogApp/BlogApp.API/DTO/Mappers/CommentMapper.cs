using BlogApp.API.DTO.Models.Comments;
using BlogApp.Domain.Entities;

namespace BlogApp.API.DTO.Mappers;

public static class CommentMapper
{
	public static Comment ToEntity(this CreateCommentRequest request) => new()
	{
		Description = request.Description,
		UserId = request.UserId,
		PostId = request.PostId,
		ParentId = request.ParentId
	};

	public static Comment ToEntity(this UpdateCommentRequest request) => new()
	{
		Description = request.Description,
		UserId = request.UserId,
		PostId = request.PostId,
		ParentId = request.ParentId
	};

	public static CommentResponse ToResponse(this Comment comment) => new(
		comment.Id,
		comment.Description,
		comment.UserId,
		comment.PostId,
		comment.ParentId,
		comment.CreatedAt,
		comment.UpdatedAt
	);
}
