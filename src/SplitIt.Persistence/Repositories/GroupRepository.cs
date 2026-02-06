using Microsoft.EntityFrameworkCore;
using SplitIt.Application.Interfaces;
using SplitIt.Domain.Entities;

namespace SplitIt.Persistence.Repositories;

public class GroupRepository : GenericRepository<Group>, IGroupRepository
{
    private readonly SplitItDbContext _context;

    public GroupRepository(SplitItDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<List<UserGroup>> GetMembersByGroupIdAsync(Guid groupId)
    {
        return await _context.UserGroups
            .Where(ug => ug.GroupId == groupId)
            .Include(ug => ug.User)
            .ToListAsync();
    }

}