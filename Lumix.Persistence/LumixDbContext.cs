using Lumix.Core.Models;
using Lumix.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace Lumix.Persistence;

public class LumixDbContext : DbContext
{
    public LumixDbContext(DbContextOptions<LumixDbContext> options) : base(options)
    {
    }

    public DbSet<RefreshTokenEntity> RefreshTokens { get; set; } = null!;
    public DbSet<UserEntity> Users { get; set; } = null!;
   

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(LumixDbContext).Assembly);
    }
}