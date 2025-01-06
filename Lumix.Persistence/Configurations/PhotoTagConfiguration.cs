using Lumix.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lumix.Persistence.Configurations
{
	public class PhotoTagConfiguration : IEntityTypeConfiguration<PhotoTag>
	{
		public void Configure(EntityTypeBuilder<PhotoTag> builder)
		{
			builder.HasKey(pt => pt.Id);

			builder.HasIndex(pt => new { pt.TagId, pt.PhotoId }).IsUnique();

			builder.HasOne(pt => pt.Photo)
				.WithMany(p => p.PhotoTags)
				.HasForeignKey(pt => pt.PhotoId)
				.OnDelete(DeleteBehavior.Restrict);

			builder.HasOne(pt => pt.Tag)
				.WithMany(t => t.PhotoTags)
				.HasForeignKey(pt => pt.TagId)
				.OnDelete(DeleteBehavior.Cascade);
		}
	}
}
