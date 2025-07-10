using Microsoft.EntityFrameworkCore;
using SDLC.Modules.UserManagement.Domain.Entities;
using SDLC.Modules.UserManagement.Infrastructure.Data.Configurations;

namespace SDLC.Modules.UserManagement.Infrastructure.Data;

public class UserManagementDbContext : DbContext
{
    public UserManagementDbContext(DbContextOptions<UserManagementDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Apply table name prefix for module isolation
        modelBuilder.HasDefaultSchema("UserManagement");

        // Apply configurations
        modelBuilder.ApplyConfiguration(new UserConfiguration());
    }
} 