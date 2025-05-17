namespace SplitIt.Domain.Entities
{
    public class UserGroup
    {
        public Guid UserId { get; set; }
        public User User { get; set; } = default!;

        public Guid GroupId { get; set; }
        public Group Group { get; set; } = default!;
        public bool IsAdmin { get; set; } = false;
        public bool IsInvitationAccepted { get; set; } = false;


    }
}
