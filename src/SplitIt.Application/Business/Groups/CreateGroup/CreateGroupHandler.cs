using MediatR;
using SplitIt.Application.Interfaces;
using SplitIt.Domain.Entities;

namespace SplitIt.Application.Business.Groups.CreateGroup;
public class CreateGroupHandler : IRequestHandler<CreateGroupCommand, Guid>
{
    private readonly IGenericRepository<Group> _genericRepo;

    public CreateGroupHandler(IGenericRepository<Group> genericRepo)
    {
        _genericRepo = genericRepo;
    }

    public async Task<Guid> Handle(CreateGroupCommand command, CancellationToken ct)
    {
        var newGroup = new Group
        {
            Id = Guid.NewGuid(),
            CreatedAt = DateTime.UtcNow,
            Name = command.Name,
            Description = command.Description,
            AdminUserId = command.AdminUserId,
        };
        await _genericRepo.AddAsync(newGroup);
        return newGroup.Id;
    }
}