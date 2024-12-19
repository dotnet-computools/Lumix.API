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

			// Unique Constraint to prevent multiple likes from same user
			builder.HasIndex(l => new { l.UserId, l.PhotoId }).IsUnique();

			builder.HasOne(l => l.User)
				.WithMany(u => u.Likes)
				.HasForeignKey(l => l.UserId)
				.OnDelete(DeleteBehavior.Restrict);

			builder.HasOne(l => l.Photo)
				.WithMany(p => p.Likes)
				.HasForeignKey(l => l.PhotoId)
				.OnDelete(DeleteBehavior.Cascade);
		}
	}
}
