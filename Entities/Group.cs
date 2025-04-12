namespace SplitIt.Entities
{
    public class Group
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<GroupUser> GroupUsers { get; set; } = new List<GroupUser>(); // Usuarios asociados a este grupo
        public ICollection<Expense> Expenses { get; set; } = new List<Expense>();
    }
}
