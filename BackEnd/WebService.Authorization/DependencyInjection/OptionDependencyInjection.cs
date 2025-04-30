using WebService.Authorization.Infrastructure.Options;

namespace WebService.Authorization.HttpApi.Host.DependencyInjection;

public static class OptionDependencyInjection
{
    public static void DependencyInjectionOption(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<DbConnectionOption>(configuration.GetSection("ConnectionStrings"));
    }
}
