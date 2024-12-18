using Lumix.Core.Models;
using Lumix.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lumix.Persistence.Configurations
{
	public class PhotoConfiguration : IEntityTypeConfiguration<Photo>
	{
		public void Configure(EntityTypeBuilder<Photo> builder)
		{
			builder.HasKey(p => p.Id);

			builder.Property(p => p.UserId)
				.IsRequired();

			builder.Property(p => p.Url)
				.IsRequired();

			builder.Property(p => p.CreatedAt)
				.IsRequired();

			builder.Property(p => p.LikeCount)
				.IsRequired();

			builder.Property(p => p.Title)
				.HasMaxLength(500);

			builder.Property(p => p.Tags)
				.HasMaxLength(500);


			builder.HasOne<User>()
				.WithMany()
				.HasForeignKey(p => p.UserId)
				.OnDelete(DeleteBehavior.Cascade);
		}
	}
}
