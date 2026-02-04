using SplitIt.Domain.Common;

namespace SplitIt.Domain.Entities
{
    public class User : BaseEntity
    {
        public string Name { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string Password { get; set; } = default!;
        public ICollection<UserGroup> UserGroups { get; set; } = new List<UserGroup>();
    }
}
