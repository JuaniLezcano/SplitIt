using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SplitIt.Domain.Common;

namespace SplitIt.Domain.Entities
{
    public class UserGroup : BaseEntity
    {
        public ICollection<User> Users { get; set; } = new List<User>();
        public ICollection<Group> Groups { get; set; } = new List<Group>();

    }
}
