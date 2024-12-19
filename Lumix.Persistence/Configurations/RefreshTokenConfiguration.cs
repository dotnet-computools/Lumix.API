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
			
			builder.Property(rt => rt.Token)
				.IsRequired()
				.HasMaxLength(500);
			
			builder.HasOne(rt => rt.User)
				.WithMany(u => u.RefreshTokens)
				.HasForeignKey(rt => rt.UserId)
				.OnDelete(DeleteBehavior.Cascade);
			
			builder.HasIndex(rt => rt.Token).IsUnique();
		}
	}
}
