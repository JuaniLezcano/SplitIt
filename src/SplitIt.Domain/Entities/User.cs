using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using SplitIt.Domain.Common;

namespace SplitIt.Domain.Entities
{
    public class User : BaseEntity
    {
        public string Name { get; set; } = default!;
        public string Email { get; set; } = default!;

        public Guid UserGroupid { get; set; }
        public UserGroup? UserGroup { get; set; }
        public ICollection<Payment> Payments { get; set; } = new List<Payment>();
    }
}
