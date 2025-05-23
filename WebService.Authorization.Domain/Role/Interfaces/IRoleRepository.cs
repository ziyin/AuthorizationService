﻿using WebService.Authorization.Domain.Role.Models;

namespace WebService.Authorization.Domain.Role.Interfaces;

public interface IRoleRepository
{
    Task<Guid> CreateAsync(RoleEntity roleEntity);
    Task UpdateAsync(RoleEntity roleEntity);
    Task<RoleEntity?> GetAsync(GetRoleParameterModel parameterModel);
    Task<IEnumerable<RoleEntity>?> GetListAsync(GetRoleListParameterModel parameterModel);
}