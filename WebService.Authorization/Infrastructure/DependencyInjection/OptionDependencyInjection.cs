using WebService.Authorization.Shard.Options;

namespace WebService.Authorization.HttpApi.Host.Infrastructure.DependencyInjection;

public static class OptionDependencyInjection
{
    public static void DependencyInjectionOption(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<DbConnectionOption>(configuration.GetSection("ConnectionStrings"));
    }
}
