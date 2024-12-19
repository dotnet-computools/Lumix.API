using Lumix.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lumix.Persistence.Configurations
{
	public class LikeConfiguration : IEntityTypeConfiguration<Like>
	{
		public void Configure(EntityTypeBuilder<Like> builder)
		{
			builder.HasKey(l => l.Id);

			builder.HasIndex(l => new { l.UserId, l.PhotoId }).IsUnique();

			builder.Property(l => l.UserId)
				.IsRequired();

			builder.Property(l => l.PhotoId)
				.IsRequired();

			builder.Property(l => l.CreatedAt)
				.IsRequired();


			builder.HasOne<User>()
				.WithMany()
				.HasForeignKey(l => l.UserId)
				.OnDelete(DeleteBehavior.Cascade);

			builder.HasOne<Photo>()
				.WithMany()
				.HasForeignKey(l => l.PhotoId)
				.OnDelete(DeleteBehavior.NoAction);
		}
	}
}
