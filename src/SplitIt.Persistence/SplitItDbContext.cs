using Microsoft.EntityFrameworkCore;
using SplitIt.Domain.Entities;

namespace SplitIt.Persistence;

public class SplitItDbContext : DbContext
{
    public SplitItDbContext(DbContextOptions<SplitItDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users {  get; set; }
    public DbSet<Expense> Expenses { get; set; }
    public DbSet<Group> Groups { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<UserGroup> UserGroups { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserGroup>()
            .HasKey(ug => new { ug.UserId, ug.GroupId });

        modelBuilder.Entity<UserGroup>()
            .HasOne(ug => ug.User)
            .WithMany(u => u.UserGroups)
            .HasForeignKey(ug => ug.UserId);

        modelBuilder.Entity<UserGroup>()
            .HasOne(ug => ug.Group)
            .WithMany(g => g.UserGroups)
            .HasForeignKey(ug => ug.GroupId);

        modelBuilder.Entity<Expense>()
            .Property(e => e.Type)
            .HasConversion<string>();
    }
}
