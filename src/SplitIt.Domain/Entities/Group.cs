using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SplitIt.Domain.Common;

namespace SplitIt.Domain.Entities
{
    public class Group : BaseEntity
    {
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public Guid UserGroupId { get; set; }
        public UserGroup? UserGroup { get; set; }
        public ICollection<Expense> Expenses { get; set; } = new List<Expense>();
    }
}
