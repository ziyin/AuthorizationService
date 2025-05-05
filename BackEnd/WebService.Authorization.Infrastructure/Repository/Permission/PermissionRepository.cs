using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using WebService.Authorization.Domain.Permission.Interface;
using WebService.Authorization.Domain.Permission.Model;
using WebService.Authorization.Shard.Options;

namespace WebService.Authorization.Infrastructure.Repository.Permission;

public class PermissionRepository
    (
        IOptions<DbConnectionOption> options
    ) : IPermissionRepository
{
    private readonly DbConnectionOption _dbConnectionOption = options.Value;

    public async Task CreateAsync(PermissionEntity permissionEntity)
    {
        var sql = @"
                    INSERT INTO Permissions
                    (
                        Code,
                        Name,
                        Enable,
                        Creator
                    )
                    VALUES
                    (
                        @Code,
                        @Name,
                        @Enable,
                        @Creator
                    );
                    ";

        using var connection = new SqlConnection(_dbConnectionOption.AuthorizationConnection);
        await connection.ExecuteAsync(sql, permissionEntity);
    }
}