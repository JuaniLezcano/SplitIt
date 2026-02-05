using SplitIt.Application.Groups.DTOs;
using SplitIt.Application.Interfaces;
using SplitIt.Domain.Entities;

namespace SplitIt.Application.Groups.UseCases;

public class CreateGroupWithMembersInteractor
{
    private readonly IGenericRepository<Group> _groupRepo;
    private readonly IUserRepository _userRepo;
    private readonly IGenericRepository<UserGroup> _userGroupRepo;

    public CreateGroupWithMembersInteractor(
        IGenericRepository<Group> groupRepo,
        IUserRepository userRepo,
        IGenericRepository<UserGroup> userGroupRepo)
    {
        _groupRepo = groupRepo;
        _userRepo = userRepo;
        _userGroupRepo = userGroupRepo;
    }

    public async Task<Guid> ExecuteAsync(CreateGroupRequestDTO req)
    {
        if (string.IsNullOrWhiteSpace(req.Name))
            throw new ArgumentException("El nombre del grupo es obligatorio.");

        // 1) validar admin existe
        var admin = await _userRepo.GetByIdAsync(req.AdminUserId);
        if (admin is null)
            throw new InvalidOperationException("AdminUserId inválido.");

        // 2) normalizar miembros (incluir admin)
        var memberIds = req.MemberUserIds?.Distinct().ToHashSet() ?? new HashSet<Guid>();
        memberIds.Add(req.AdminUserId);

        // 3) validar que todos existan
        foreach (var id in memberIds)
        {
            var u = await _userRepo.GetByIdAsync(id);
            if (u is null)
                throw new InvalidOperationException($"Usuario no encontrado: {id}");
        }

        // 4) crear grupo
        var group = new Group { Name = req.Name, Description = req.Description };
        await _groupRepo.AddAsync(group);

        // 5) crear UserGroups
        foreach (var id in memberIds)
        {
            var ug = new UserGroup
            {
                UserId = id,
                GroupId = group.Id,
                IsAdmin = id == req.AdminUserId,
            };
            await _userGroupRepo.AddAsync(ug);
        }

        return group.Id;
    }
}
