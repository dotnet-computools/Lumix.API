﻿using Lumix.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lumix.Persistence.Configurations
{
	public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
	{
		public void Configure(EntityTypeBuilder<RefreshToken> builder)
		{
			builder.HasKey(rt => rt.Id);

			builder.Property(rt => rt.UserId)
				.IsRequired();

			builder.Property(rt => rt.Token)
				.IsRequired();

			builder.Property(rt => rt.ExpiresAt)
				.IsRequired();

			builder.Property(rt => rt.CreatedAt)
				.IsRequired();

			builder.HasOne<User>()
				.WithMany()
				.HasForeignKey(rt => rt.UserId)
				.OnDelete(DeleteBehavior.Cascade);
		}
	}
}
