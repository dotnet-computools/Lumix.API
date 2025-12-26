using Lumix.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lumix.Persistence.Configurations
{
	public class CommentConfiguration : IEntityTypeConfiguration<Comment>
	{
		public void Configure(EntityTypeBuilder<Comment> builder)
		{
			builder.HasKey(c => c.Id);
			
			builder.Property(c => c.Text)
				.IsRequired()
				.HasMaxLength(1000);

			builder.HasOne(c => c.User)
				.WithMany(u => u.Comments)
				.HasForeignKey(c => c.UserId)
				.OnDelete(DeleteBehavior.Restrict);

			builder.HasOne(c => c.Photo)
				.WithMany(p => p.Comments)
				.HasForeignKey(c => c.PhotoId)
				.OnDelete(DeleteBehavior.Cascade);

			builder.HasOne(c => c.Parent)
				.WithMany(c => c.Children)
				.HasForeignKey(c => c.ParentId)
				.OnDelete(DeleteBehavior.NoAction);
		}
	}
}
