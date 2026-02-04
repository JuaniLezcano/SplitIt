using SplitIt.Domain.Common;

namespace SplitIt.Domain.Entities
{
    public class UserGroup : BaseEntity
    {
        public Guid UserId { get; set; }
        public User User { get; set; } = default!;

        public Guid GroupId { get; set; }
        public Group Group { get; set; } = default!;
        public bool IsAdmin { get; set; } = false;
        public bool IsInvitationAccepted { get; set; } = false;

        public ICollection<Payment> Payments { get; set; } = new List<Payment>();
        public ICollection<ExpenseSplit> ExpenseSplits { get; set; } = new List<ExpenseSplit>();
        public ICollection<Expense> CreatedExpenses { get; set; } = new List<Expense>();
    }
}
