using BlogApp.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace BlogApp.API.DTO.Models.Users;

public record CreateUserRequest(
	[property: Required, MaxLength(100)] string UserName,
	[property: Range(1, 150)] int Age,
	[property: Required, MaxLength(200)] string FullName,
	UserType Role = UserType.Default
);
