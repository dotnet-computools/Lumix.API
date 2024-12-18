using Lumix.Core.Models;
using Lumix.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lumix.Persistence.Configurations
{
	public class UserConfiguration : IEntityTypeConfiguration<User>
	{
		public void Configure(EntityTypeBuilder<User> builder)
		{
			builder.HasKey(u => u.Id);

			builder.Property(u => u.UserName)
				.IsRequired()
				.HasMaxLength(30);

			builder.Property(u => u.Email)
				.IsRequired()
				.HasMaxLength(254);

			builder.Property(u => u.PasswordHash)
				.IsRequired()
				.HasMaxLength(64);

			builder.Property(u => u.CreatedAt)
				.IsRequired();

			builder.Property(u => u.ProfilePictureUrl)
				.IsRequired(false);

			builder.Property(u => u.Bio)
				.IsRequired(false)
				.HasMaxLength(150);

			builder.HasIndex(u => u.UserName)
				.IsUnique();

			builder.HasIndex(u => u.Email)
				.IsUnique();
		}
	}
}
