namespace KingPrice.Core;

using Entities;
using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }
    
    public DbSet<Group> Groups => this.Set<Group>();
    
    public DbSet<User> Users => this.Set<User>();
    
    public DbSet<Permission> Permissions => this.Set<Permission>();
}
