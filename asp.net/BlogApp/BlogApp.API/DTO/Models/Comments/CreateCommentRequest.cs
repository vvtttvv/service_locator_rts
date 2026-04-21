using System.ComponentModel.DataAnnotations;

namespace BlogApp.API.DTO.Models.Comments;

public record CreateCommentRequest(
	[property: Required, MaxLength(2000)] string Description,
	[property: Required] Guid UserId,
	[property: Required] Guid PostId,
	Guid? ParentId
);
