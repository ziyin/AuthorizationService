using Mapster;
using WebService.Authorization.Application.Contracts.ResponseDtos.Permission;
using WebService.Authorization.Application.Contracts.ResponseDtos.Role;
using WebService.Authorization.HttpApi.Repsonse.Permission;
using WebService.Authorization.HttpApi.Repsonse.Role;

namespace WebService.Authorization.HttpApi.Host.Infrastructure
{
    public class ControllerMappingConfig
    {
        public static void RegisterMappings()
        {
            TypeAdapterConfig<RolePermissionDto, RolePermissionResponse>.NewConfig();
            TypeAdapterConfig<RoleDto, RoleResponse>.NewConfig();
            TypeAdapterConfig<PermissionDto, PermissionResponse>.NewConfig();
        }
    }
}