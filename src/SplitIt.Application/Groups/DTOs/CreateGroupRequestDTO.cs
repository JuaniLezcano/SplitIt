namespace SplitIt.Application.Groups.DTOs;
public class CreateGroupRequestDTO
{
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public Guid AdminUserId { get; set; }
    public List<Guid> MemberUserIds { get; set; } = new();
}
