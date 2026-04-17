using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Store.Domain.Entities;

namespace Store.PostgreSQL.Configurations;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
	public const string TableName = "categories";

	public void Configure(EntityTypeBuilder<Category> builder)
	{
		builder.ToTable(TableName);
		builder.HasKey(x => x.Id);
		builder.Property(x => x.Name).IsRequired().HasMaxLength(100);
		builder.HasIndex(x => x.Name).IsUnique();
	}
}