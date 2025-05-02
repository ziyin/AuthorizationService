using WebService.Authorization.Application.AppService;
using WebService.Authorization.Application.Contracts.Interfaces;
using WebService.Authorization.Domain.User.Interfaces;
using WebService.Authorization.Infrastructure.Repository;

namespace WebService.Authorization.HttpApi.Host.DependencyInjection;

public static class ClassDependencyInjection
{
    public static void DependencyInjectionClass(this IServiceCollection services)
    {
        #region AppService

        services.AddTransient<IUserAppService, UserAppService>();

        #endregion

        #region repository

        services.AddScoped<IUserRepository, UserRepository>();

        #endregion
    }
}