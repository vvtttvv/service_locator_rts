using System.ComponentModel.DataAnnotations;

namespace BlogApp.API.DTO.Models.Posts;

public record CreatePostRequest(
	[property: Required, MaxLength(200)] string Title,
	[property: MaxLength(2000)] string? Description,
	[property: Required] Guid UserId
);
