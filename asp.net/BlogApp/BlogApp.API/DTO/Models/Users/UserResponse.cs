using BlogApp.Domain.Enums;

namespace BlogApp.API.DTO.Models.Users;

public record UserResponse(
	Guid Id,
	string UserName,
	int Age,
	string FullName,
	UserType Role,
	DateTime CreatedAt,
	DateTime UpdatedAt
);
