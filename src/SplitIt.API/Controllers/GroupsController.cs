using Microsoft.AspNetCore.Mvc;
using SplitIt.Application.Groups.DTOs;
using SplitIt.Application.Groups.UseCases;
using SplitIt.Persistence;

[ApiController]
[Route("api/[controller]")]
public class GroupsController : ControllerBase
{
    private readonly CreateGroupWithMembersInteractor _create;

    public GroupsController(CreateGroupWithMembersInteractor create)
    {
        _create = create;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateGroupRequestDTO req)
    {
        var groupId = await _create.ExecuteAsync(req);
        return Ok(new { GroupId = groupId });
    }
}
