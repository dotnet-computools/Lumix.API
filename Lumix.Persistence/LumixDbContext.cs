using Lumix.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace Lumix.Persistence
{
	public class LumixDbContext : DbContext
	{
		public LumixDbContext(DbContextOptions<LumixDbContext> options) : base(options) { }
		public LumixDbContext() { }
		public DbSet<User> Users { get; set; }
		public DbSet<Photo> Photos { get; set; }
		public DbSet<Like> Likes { get; set; }
		public DbSet<Comment> Comments { get; set; }
		public DbSet<Follow> Follows { get; set; }
		public DbSet<RefreshToken> RefreshTokens { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfigurationsFromAssembly(typeof(LumixDbContext).Assembly);
		}
	}
}
