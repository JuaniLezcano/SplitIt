using SplitIt.Domain.Common;

namespace SplitIt.Domain.Entities
{
    public class Group : BaseEntity
    {
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public Guid AdminUserId { get; set; }
        public ICollection<UserGroup> UserGroups { get; set; } = new List<UserGroup>();
        public ICollection<Expense> Expenses { get; set; } = new List<Expense>();
    }
}
