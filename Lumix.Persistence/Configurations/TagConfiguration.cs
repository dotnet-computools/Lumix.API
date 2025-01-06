using Lumix.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lumix.Persistence.Configurations
{
	public class TagConfiguration : IEntityTypeConfiguration<Tag>
	{
		public void Configure(EntityTypeBuilder<Tag> builder)
		{
			builder.HasKey(t => t.Id);

			builder.HasIndex(t => t.Name)
				.IsUnique();

			builder.Property(t => t.Name)
				.IsRequired()
				.HasMaxLength(30);

			builder.Property(t => t.CreatedAt)
				.IsRequired();


			builder.HasMany(t => t.PhotoTags)
				.WithOne(pt => pt.Tag)
				.HasForeignKey(pt => pt.TagId)
				.OnDelete(DeleteBehavior.Cascade);
		}
	}
}
