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
        // Configuración de UserGroup con PK compuesta
        modelBuilder.Entity<UserGroup>()
            .HasKey(ug => new { ug.UserId, ug.GroupId });

        modelBuilder.Entity<UserGroup>()
            .HasOne(ug => ug.User)
            .WithMany(u => u.UserGroups)
            .HasForeignKey(ug => ug.UserId)
            .OnDelete(DeleteBehavior.Cascade); //  Si se elimina un usuario, se eliminan sus relaciones con grupos

        modelBuilder.Entity<UserGroup>()
            .HasOne(ug => ug.Group)
            .WithMany(g => g.UserGroups)
            .HasForeignKey(ug => ug.GroupId)
            .OnDelete(DeleteBehavior.Cascade); // Si se elimina un grupo, se eliminan sus relaciones con usuarios

        modelBuilder.Entity<Expense>()
            .HasOne(u => u.CreatedBy)
            .WithMany()
            .HasForeignKey(u => u.CreatedById)
            .OnDelete(DeleteBehavior.Restrict); // Evita que se borre un usuario si tiene gastos creados

        modelBuilder.Entity<Expense>()
            .HasOne(e => e.Group)
            .WithMany(g => g.Expenses)
            .HasForeignKey(e => e.GroupId)
            .OnDelete(DeleteBehavior.Cascade); // Si se borra el grupo, se borran los gastos

        modelBuilder.Entity<Payment>()
            .HasOne(p => p.User)
            .WithMany(u => u.Payments)
            .HasForeignKey(p => p.UserId)
            .OnDelete(DeleteBehavior.Restrict); // No permitir borrar un usuario si tiene pagos

        modelBuilder.Entity<Expense>()
            .Property(e => e.Type)
            .HasConversion<string>();

        // Permite la convencion de las tablas en singular, manejando las colecciones del DbSet en plural.
        base.OnModelCreating(modelBuilder);
        foreach (var entity in modelBuilder.Model.GetEntityTypes())
        {
            // Tomar solo el nombre de la clase (sin namespaces)
            var tableName = entity.ClrType.Name;
            modelBuilder.Entity(entity.ClrType).ToTable(tableName);
        }
    }
}
