namespace BlogApp.API.DTO.Models.Comments;

public record CommentResponse(
	Guid Id,
	string Description,
	Guid UserId,
	Guid PostId,
	Guid? ParentId,
	DateTime CreatedAt,
	DateTime UpdatedAt
);
