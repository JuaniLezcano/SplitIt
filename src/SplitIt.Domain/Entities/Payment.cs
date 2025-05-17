using SplitIt.Domain.Common;

namespace SplitIt.Domain.Entities
{
    public class Payment : BaseEntity
    {
        public Guid UserId { get; set; }
        public User? User { get; set; }

        public Guid ExpenseId { get; set; }
        public Expense? Expense { get; set; }

        public decimal Amount { get; set; }
    }
}
