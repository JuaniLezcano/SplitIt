namespace SplitIt.Application.Business.Groups.DTOs;

public class GroupMemberDTO
{
    public Guid UserId { get; set; }
    public string Name { get; set; } = default!;
    public string Email { get; set; } = default!;
    public bool IsAdmin { get; set; }
    public bool IsInvitationAccepted { get; set; }
}