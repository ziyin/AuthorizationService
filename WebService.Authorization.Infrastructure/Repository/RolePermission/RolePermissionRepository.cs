using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using WebService.Authorization.Domain.RolePermission.Interface;
using WebService.Authorization.Domain.RolePermission.Model;
using WebService.Authorization.Shard.Options;

namespace WebService.Authorization.Infrastructure.Repository.RolePermission;

public class RolePermissionRepository
    (
        IOptions<DbConnectionOption> options
    ) : IRolePermissionRepository
{
    private readonly DbConnectionOption _dbConnectionOption = options.Value;

    public async Task CreateAsync(RolePermissionEntity rolePermissionEntity)
    {
        var sql = @"
                    INSERT INTO RolePermissions
                    (
                        RoleId, 
                        PermissionId,
                        Creator
                    )
                    VALUES
                    (
                        @RoleId,
                        @PermissionId, 
                        @Creator
                    )
                   ";

        using var connection = new SqlConnection(_dbConnectionOption.AuthorizationConnection);
        await connection.ExecuteAsync(sql, rolePermissionEntity);
    }

    public async Task CreateManyAsync(IEnumerable<RolePermissionEntity> rolePermissionEntity)
    {
        var sql = @"
                    INSERT INTO RolePermissions
                    (
                        RoleId, 
                        PermissionId,
                        Creator
                    )
                    VALUES
                    (
                        @RoleId,
                        @PermissionId, 
                        @Creator
                    )
                   ";

        using var connection = new SqlConnection(_dbConnectionOption.AuthorizationConnection);
        await connection.ExecuteAsync(sql, rolePermissionEntity);
    }

    public async Task<IEnumerable<RolePermissionEntity>?> GetListAsync(GetRolePermissionListParameterModel parameterModel)
    {
        var sqlBaseCommand = @"SELECT 
                                	RP.Id,
                                	RP.RoleId,
                                	RP.PermissionId,
                                	Permissions.[Code] as PermissionCode,
                                	Permissions.[Name] as PermissionName,
                                	RP.CreateTime,
                                	RP.Creator
                                FROM RolePermissions RP WITH(NOLOCK)
                                JOIN Permissions on Permissions.Id=RP.PermissionId
                                JOIN Roles on Roles.Id=RP.RoleId
                                WHERE Roles.[Enable]=1 AND Permissions.[Enable]=1 ";

        var sqlbuilder = new RolePermissionSqlBuilder<GetRolePermissionListParameterModel>(sqlBaseCommand, parameterModel)
            .QueryRoleId()
            .QueryPermissionId()
            .QueryCode();
        var sql = sqlbuilder.BuildSql();
        var parameters = sqlbuilder.BuildParameters();
        using var conn = new SqlConnection(_dbConnectionOption.AuthorizationConnection);
        var result = await conn.QueryAsync<RolePermissionEntity>(sql, parameters);
        return result;
    }
}
