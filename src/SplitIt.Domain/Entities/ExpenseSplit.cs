using SplitIt.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace SplitIt.Domain.Entities
{
    public class ExpenseSplit : BaseEntity
    {
        public Guid UserGroupId { get; set; }
        public UserGroup UserGroup { get; set; } = default!;

        public Guid ExpenseId { get; set; }
        public Expense Expense { get; set; } = default!;
        public decimal OwedAmount { get; set; }
    }
}
