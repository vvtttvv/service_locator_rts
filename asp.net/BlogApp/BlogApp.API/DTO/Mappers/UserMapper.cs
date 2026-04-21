using BlogApp.API.DTO.Models.Users;
using BlogApp.Domain.Entities;

namespace BlogApp.API.DTO.Mappers;

public static class UserMapper
{
	public static User ToEntity(this CreateUserRequest request) => new()
	{
		UserName = request.UserName,
		Age = request.Age,
		FullName = request.FullName,
		Role = request.Role
	};

	public static User ToEntity(this UpdateUserRequest request) => new()
	{
		UserName = request.UserName,
		Age = request.Age,
		FullName = request.FullName,
		Role = request.Role
	};

	public static UserResponse ToResponse(this User user) => new(
		user.Id,
		user.UserName,
		user.Age,
		user.FullName,
		user.Role,
		user.CreatedAt,
		user.UpdatedAt
	);
}
