using SplitIt.Domain.Common;
using SplitIt.Domain.Enums;

namespace SplitIt.Domain.Entities
{
    public class Expense : BaseEntity
    {
        public string Description { get; set; } = default!;
        public Guid CreatedByUserGroupId { get; set; }
        public UserGroup CreatedByUserGroup { get; set; } = default!;
        public decimal Amount { get; set; }
        public Guid GroupId { get; set; }
        public Group? Group { get; set; }
        public ExpenseType Type { get; set; } = ExpenseType.Equal;
        public ICollection<Payment> Payments { get; set; } = new List<Payment>();
        public ICollection<ExpenseSplit> ExpenseSplits { get; set; } = new List<ExpenseSplit>();
    }
}
