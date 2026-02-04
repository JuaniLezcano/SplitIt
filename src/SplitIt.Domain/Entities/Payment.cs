using SplitIt.Domain.Common;

namespace SplitIt.Domain.Entities
{
    public class Payment : BaseEntity
    {
        public Guid UserGroupId { get; set; }
        public UserGroup? UserGroup { get; set; }

        public Guid ExpenseId { get; set; }
        public Expense? Expense { get; set; }

        public decimal Amount { get; set; }
    }
}
