using Microsoft.EntityFrameworkCore;
using SplitIt.Entities;

namespace SplitIt.Data
{
    public class SplitItDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql("Host=colosal.duckdns.org;Port=14998;Database=S31-Grupo8-Banco-Generacion;Persist Security Info=True;Password=tallersoft600;Username=postgres");
    
        public DbSet<User> Users => Set<User>();
        public DbSet<Group> Groups => Set<Group>();
        public DbSet<Expense> Expenses => Set<Expense>();
        public DbSet<GroupUser> GroupUsers => Set<GroupUser>();
    }
}
