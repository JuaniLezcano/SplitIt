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

        public ICollection<Group> Groups { get; set; } = new List<Group>();
        public ICollection<Payment> Payments { get; set; } = new List<Payment>();
    }
}
