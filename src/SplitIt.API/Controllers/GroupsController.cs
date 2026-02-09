using Microsoft.AspNetCore.Mvc;
using SplitIt.Application.Business.Groups.DTOs;
using SplitIt.Application.Business.Groups.UseCases;
using SplitIt.Persistence;

[ApiController]
[Route("api/[controller]")]
public class GroupsController : ControllerBase
{
    private readonly CreateGroupWithMembersInteractor _create;
    private readonly GetGroupMembersInteractor _getMembers;

    public GroupsController(CreateGroupWithMembersInteractor create, GetGroupMembersInteractor getMembers)
    {
        _create = create;
        _getMembers = getMembers;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateGroupRequestDTO req)
    {
        var groupId = await _create.ExecuteAsync(req);
        return Ok(new { GroupId = groupId });
    }

    [HttpGet("{groupId:guid}")]
    public async Task<IActionResult> GetMembers(Guid groupId)
    {
        var members = await _getMembers.ExecuteAsync(groupId);
        return Ok(members);
    }
}
