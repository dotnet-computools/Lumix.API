using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Lumix.Persistence.Entities;

namespace Lumix.Persistence.Configurations
{
	public class FollowConfiguration : IEntityTypeConfiguration<Follow>
	{
		public void Configure(EntityTypeBuilder<Follow> builder)
		{
			builder.HasKey(f => f.Id);

			builder.Property(f => f.FollowerId)
				.IsRequired();

			builder.Property(f => f.FollowingId)
				.IsRequired();

			builder.Property(f => f.CreatedAt)
				.IsRequired();

			builder.HasOne<User>()
				.WithMany()
				.HasForeignKey(f => f.FollowerId)
				.OnDelete(DeleteBehavior.Cascade);

			builder.HasOne<User>()
				.WithMany()
				.HasForeignKey(f => f.FollowingId)
				.OnDelete(DeleteBehavior.NoAction);
		}
	}
}
