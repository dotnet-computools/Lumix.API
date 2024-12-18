using Lumix.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Comment = Lumix.Persistence.Entities.Comment;
using Photo = Lumix.Persistence.Entities.Photo;

namespace Lumix.Persistence.Configurations
{
	public class CommentConfiguration : IEntityTypeConfiguration<Comment>
	{
		public void Configure(EntityTypeBuilder<Comment> builder)
		{
			builder.HasKey(c => c.Id);

			builder.Property(c => c.UserId)
				.IsRequired();

			builder.Property(c => c.PhotoId)
				.IsRequired();

			builder.Property(c => c.Text)
				.IsRequired()
				.HasMaxLength(250);

			builder.Property(c => c.CreatedAt)
				.IsRequired();


			builder.HasOne<User>()
				.WithMany()
				.HasForeignKey(c => c.UserId)
				.OnDelete(DeleteBehavior.Cascade);

			builder.HasOne<Photo>()
				.WithMany()
				.HasForeignKey(c => c.PhotoId)
				.OnDelete(DeleteBehavior.NoAction);
		}
	}
}
