using WebService.Authorization.Application.AppService;
using WebService.Authorization.Application.Contracts.Interfaces;
using WebService.Authorization.Domain.Login;
using WebService.Authorization.Domain.Login.Interfaces;
using WebService.Authorization.Domain.Permission.Interface;
using WebService.Authorization.Domain.Role.Interfaces;
using WebService.Authorization.Domain.RolePermission;
using WebService.Authorization.Domain.RolePermission.Interface;
using WebService.Authorization.Domain.User.Interfaces;
using WebService.Authorization.Domain.UserRole;
using WebService.Authorization.Domain.UserRole.Interface;
using WebService.Authorization.Infrastructure.Repository.Permission;
using WebService.Authorization.Infrastructure.Repository.Role;
using WebService.Authorization.Infrastructure.Repository.RolePermission;
using WebService.Authorization.Infrastructure.Repository.User;
using WebService.Authorization.Infrastructure.Repository.UserRole;

namespace WebService.Authorization.HttpApi.Host.DependencyInjection;

public static class ClassDependencyInjection
{
    public static void DependencyInjectionClass(this IServiceCollection services)
    {
        services.AppSerivceDependencyInjection();
        services.ManagerDependencyInjection();
        services.RepositoryDependencyInjection();
    }

    #region --Private

    private static void AppSerivceDependencyInjection(this IServiceCollection services)
    {
        services.AddTransient<ILoginAppService, LoginAppService>();
        services.AddTransient<IPermissionAppService, PermissionAppService>();
        services.AddTransient<IRoleAppService, RoleAppService>();
        services.AddTransient<IRolePermissionAppService, RolePermissionAppService>();
        services.AddTransient<IUserInformationAppService, UserInformationAppService>();
        services.AddTransient<IUserMaintainAppService, UserMaintainAppService>();
        services.AddTransient<IUserRoleAppService, UserRoleAppService>();
    }

    private static void ManagerDependencyInjection(this IServiceCollection services)
    {
        services.AddTransient<IValidateLoginManager, ValidateLoginManager>();
        services.AddTransient<IBindPermissionToRoleManager, BindPermissionToRoleManager>();
        services.AddTransient<IBindUserToRoleManager, BindUserToRoleManager>();
    }

    private static void RepositoryDependencyInjection(this IServiceCollection services)
    {
        services.AddScoped<IPermissionRepository, PermissionRepository>();
        services.AddScoped<IRoleRepository, RoleRepository>();
        services.AddScoped<IRolePermissionRepository, RolePermissionRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUserRoleRepository, UserRoleRepository>();
    }

    #endregion
}