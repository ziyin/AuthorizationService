using Mapster;
using WebService.Authorization.Domain.User.Models;

namespace WebService.Authorization.Application;

public static class ApplicationMappingConfig
{
    public static void RegisterMappings()
    {
        TypeAdapterConfig<UserDataModel, UserEntity>.NewConfig();
    }
}