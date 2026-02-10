using MediatR;
using Microsoft.AspNetCore.Mvc;
using SplitIt.Application.Business.Groups.CreateGroup;


[ApiController]
[Route("api/[controller]")]
public class GroupsController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateGroupCommand command, CancellationToken ct)
    {
        var groupId = await mediator.Send(command, ct);
        return Ok(new { GroupId = groupId });
    }
}
