namespace BlogApp.API.DTO.Models.Posts;

public record PostResponse(
	Guid Id,
	string Title,
	string Description,
	Guid UserId,
	DateTime CreatedAt,
	DateTime UpdatedAt
);
