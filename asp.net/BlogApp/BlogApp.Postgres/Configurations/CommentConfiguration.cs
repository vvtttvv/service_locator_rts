using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BlogApp.Domain.Entities;

namespace BlogApp.Postgres.Configurations;

public class CommentConfiguration : IEntityTypeConfiguration<Comment>
{
	public const string TableName = "comments";

	public void Configure(EntityTypeBuilder<Comment> builder)
	{
		builder.ToTable(TableName);
		builder.HasKey(x => x.Id);
		builder.Property(x => x.Description).IsRequired();

		builder.HasOne(x => x.Post)
			.WithMany(x => x.Comments)
			.HasForeignKey(x => x.PostId)
			.OnDelete(DeleteBehavior.Cascade);
		
		builder.HasOne(x => x.User)
			.WithMany(x=> x.Comments)
			.HasForeignKey(x => x.UserId)
			.OnDelete(DeleteBehavior.Cascade);
		
		// Self Relations were new for me
		builder.HasOne(x => x.Parent)
			.WithMany(x => x.Replies)
			.HasForeignKey(x => x.ParentId)
			.OnDelete(DeleteBehavior.Restrict);
	}
}