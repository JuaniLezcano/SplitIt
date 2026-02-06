using SplitIt.Domain.Entities;

namespace SplitIt.Application.Interfaces;

public interface IGroupRepository : IGenericRepository<Group>
{
    Task<List<UserGroup>> GetMembersByGroupIdAsync(Guid groupId);
}  