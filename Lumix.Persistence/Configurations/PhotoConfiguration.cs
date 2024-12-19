﻿using Lumix.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lumix.Persistence.Configurations
{
	public class PhotoConfiguration : IEntityTypeConfiguration<Photo>
	{
		public void Configure(EntityTypeBuilder<Photo> builder)
		{
			builder.HasKey(p => p.Id);
			
			builder.Property(p => p.Title)
				.IsRequired()
				.HasMaxLength(200);

			builder.Property(p => p.Url)
				.IsRequired()
				.HasMaxLength(500);

			builder.Property(p => p.Tags)
				.HasMaxLength(500);

			
			builder.HasOne(p => p.User)
				.WithMany(u => u.Photos)
				.HasForeignKey(p => p.UserId)
				.OnDelete(DeleteBehavior.Cascade);

			builder.HasMany(p => p.Likes)
				.WithOne(l => l.Photo)
				.HasForeignKey(l => l.PhotoId)
				.OnDelete(DeleteBehavior.Cascade);

			builder.HasMany(p => p.Comments)
				.WithOne(c => c.Photo)
				.HasForeignKey(c => c.PhotoId)
				.OnDelete(DeleteBehavior.Cascade);
		}
	}
}
