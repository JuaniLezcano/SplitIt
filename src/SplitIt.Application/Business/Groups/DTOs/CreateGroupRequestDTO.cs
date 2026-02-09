namespace SplitIt.Application.Business.Groups.DTOs;
public class CreateGroupRequestDTO
{
    public required string Name { get; set; }
    public string? Description { get; set; }
    public Guid AdminUserId { get; set; }
    public List<Guid> MemberUserIds { get; set; } = new();
}
