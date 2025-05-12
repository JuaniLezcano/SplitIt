using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SplitIt.Domain.Common;
using SplitIt.Domain.Enums;

namespace SplitIt.Domain.Entities
{
    public class Expense : BaseEntity
    {
        public string Description { get; set; } = default!;
        public Guid CreatedById { get; set; }
        public User CreatedBy { get; set; } = default!;
        public decimal Amount { get; set; }
        public Guid GroupId { get; set; }
        public Group? Group { get; set; }
        public ExpenseType Type { get; set; } = ExpenseType.Equal;
        public ICollection<Payment> Payments { get; set; } = new List<Payment>();
    }
}
