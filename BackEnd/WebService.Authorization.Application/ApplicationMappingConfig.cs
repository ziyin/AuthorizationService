using Mapster;
using WebService.Authorization.Application.Contracts.ResponseDtos.Permission;
using WebService.Authorization.Application.Contracts.ResponseDtos.Role;
using WebService.Authorization.Application.Contracts.ResponseDtos.User;
using WebService.Authorization.Domain.Permission.Model;
using WebService.Authorization.Domain.Role.Models;
using WebService.Authorization.Domain.RolePermission.Model;
using WebService.Authorization.Domain.User.Models;

namespace WebService.Authorization.Application;

public static class ApplicationMappingConfig
{
    public static void RegisterMappings()
    {
        TypeAdapterConfig<UserEntity, UserDto>.NewConfig();
        TypeAdapterConfig<RolePermissionEntity, RolePermissionDto>.NewConfig();
        TypeAdapterConfig<RoleEntity, RoleDto>.NewConfig();
        TypeAdapterConfig<PermissionEntity, PermissionDto>.NewConfig();
    }
}