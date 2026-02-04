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
    public DbSet<ExpenseSplit> ExpenseSplits { get; set; }  

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // -------- UserGroup -------- 
        modelBuilder.Entity<UserGroup>()
            .HasKey(ug => ug.Id);

        modelBuilder.Entity<UserGroup>()
            .HasIndex(ug => new { ug.UserId, ug.GroupId })
            .IsUnique();

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

        // -------- Expense --------
        modelBuilder.Entity<Expense>()
            .HasOne(e => e.CreatedByUserGroup)
            .WithMany(ug => ug.CreatedExpenses)
            .HasForeignKey(e => e.CreatedByUserGroupId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Expense>()
            .HasOne(e => e.Group)
            .WithMany(g => g.Expenses)
            .HasForeignKey(e => e.GroupId)
            .OnDelete(DeleteBehavior.Cascade); // Si se borra el grupo, se borran los gastos

        modelBuilder.Entity<Expense>()
            .Property(e => e.Type)
            .HasConversion<string>();

        modelBuilder.Entity<Expense>()
            .Property(e => e.Amount)
            .HasPrecision(18, 2);

        // -------- Payment --------
        modelBuilder.Entity<Payment>()
            .HasOne(p => p.UserGroup)
            .WithMany(ug => ug.Payments)
            .HasForeignKey(p => p.UserGroupId)
            .OnDelete(DeleteBehavior.Restrict); // No borrar UserGroup si tiene pagos

        modelBuilder.Entity<Payment>()
            .HasOne(p => p.Expense)
            .WithMany(e => e.Payments)
            .HasForeignKey(p => p.ExpenseId)
            .OnDelete(DeleteBehavior.Cascade);  // Borrar pagos si se borra el gasto

        modelBuilder.Entity<Expense>()
            .Property(e => e.Type)
            .HasConversion<string>();

        // ---------- ExpenseSplit ----------
        modelBuilder.Entity<ExpenseSplit>()
            .HasKey(es => es.Id);

        // Un split por usuario-en-grupo por gasto (evita duplicados)
        modelBuilder.Entity<ExpenseSplit>()
            .HasIndex(es => new { es.ExpenseId, es.UserGroupId })
            .IsUnique();

        modelBuilder.Entity<ExpenseSplit>()
            .HasOne(es => es.Expense)
            .WithMany(e => e.ExpenseSplits)
            .HasForeignKey(es => es.ExpenseId)
            .OnDelete(DeleteBehavior.Cascade); // si borrás el gasto, borrás sus splits

        modelBuilder.Entity<ExpenseSplit>()
            .HasOne(es => es.UserGroup)
            .WithMany(ug => ug.ExpenseSplits)
            .HasForeignKey(es => es.UserGroupId)
            .OnDelete(DeleteBehavior.Restrict); // evita borrar un UserGroup si tiene historial

        modelBuilder.Entity<ExpenseSplit>()
            .Property(es => es.OwedAmount)
            .HasPrecision(18, 2);

        // -------- User --------
        modelBuilder.Entity<User>()
            .HasIndex(u => u.Email)
            .IsUnique();

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
