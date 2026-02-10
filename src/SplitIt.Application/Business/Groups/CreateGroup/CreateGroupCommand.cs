using MediatR;

namespace SplitIt.Application.Business.Groups.CreateGroup;

public record CreateGroupCommand(
    string Name,
    string Description,
    Guid AdminUserId
    ) : IRequest<Guid>;
