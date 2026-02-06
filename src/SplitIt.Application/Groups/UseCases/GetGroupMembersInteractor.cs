using System;
using System.Collections.Generic;
using System.Text;
using SplitIt.Application.Groups.DTOs;
using SplitIt.Application.Interfaces;

namespace SplitIt.Application.Groups.UseCases;

public class GetGroupMembersInteractor
{
    private readonly IGroupRepository _groupRepo;

    public GetGroupMembersInteractor(IGroupRepository groupRepo)
    {
        _groupRepo = groupRepo;
    }

    public async Task<List<GroupMemberDTO>> ExecuteAsync(Guid groupId)
    {
        var group = await _groupRepo.GetByIdAsync(groupId);
        if (group is null)
            throw new InvalidOperationException($"Grupo no encontrado: {groupId}");

        var userGroups = await _groupRepo.GetMembersByGroupIdAsync(groupId);

        return userGroups.Select(ug => new GroupMemberDTO
        {
            UserId = ug.UserId,
            Name = ug.User.Name,
            Email = ug.User.Email,
            IsAdmin = ug.IsAdmin,
            IsInvitationAccepted = ug.IsInvitationAccepted
        }).ToList();
    }
}
