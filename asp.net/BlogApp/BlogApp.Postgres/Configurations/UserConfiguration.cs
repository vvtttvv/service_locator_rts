using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BlogApp.Domain.Entities;

namespace BlogApp.Postgres.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
	public const string TableName = "users";

	public void Configure(EntityTypeBuilder<User> builder)
	{
		builder.ToTable(TableName);
		builder.HasKey(x => x.Id);
		builder.Property(x => x.UserName).IsRequired().HasMaxLength(50);
		builder.HasIndex(x => x.UserName).IsUnique();
	}
}