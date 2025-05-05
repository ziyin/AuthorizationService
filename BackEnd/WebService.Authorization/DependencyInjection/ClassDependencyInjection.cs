using WebService.Authorization.Application.AppService;
using WebService.Authorization.Application.Contracts.Interfaces;
using WebService.Authorization.Domain.Login;
using WebService.Authorization.Domain.Login.Interfaces;
using WebService.Authorization.Domain.Role.Interfaces;
using WebService.Authorization.Domain.User;
using WebService.Authorization.Domain.User.Interfaces;
using WebService.Authorization.Domain.UserRole.Interface;
using WebService.Authorization.Infrastructure.Repository.Role;
using WebService.Authorization.Infrastructure.Repository.User;
using WebService.Authorization.Infrastructure.Repository.UserRole;

namespace WebService.Authorization.HttpApi.Host.DependencyInjection;

public static class ClassDependencyInjection
{
    public static void DependencyInjectionClass(this IServiceCollection services)
    {
        #region AppService

        services.AddTransient<ILoginAppService, LoginAppService>();
        services.AddTransient<IRoleAppService, RoleAppService>();
        services.AddTransient<IUserInformationAppService, UserInformationAppService>();
        services.AddTransient<IUserMaintainAppService, UserMaintainAppService>();
        services.AddTransient<IUserRoleAppService, UserRoleAppService > ();

        #endregion

        #region Manager

        services.AddTransient<IValidateLoginManager, ValidateLoginManager>();
        services.AddTransient<IBindUserToRoleManager, BindUserToRoleManager>();

        #endregion

        #region repository

        services.AddScoped<IRoleRepository, RoleRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUserRoleRepository, UserRoleRepository>();

        #endregion
    }
}