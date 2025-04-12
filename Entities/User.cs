namespace SplitIt.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;

        public ICollection<GroupUser> GroupUsers { get; set; } = new List<GroupUser>(); // Grupos asociados a este usuario
        public ICollection<Expense> Expenses { get; set; } = new List<Expense>(); // Gastos que creo este usuario
    }
}
