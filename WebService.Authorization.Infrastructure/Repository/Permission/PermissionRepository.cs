using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using WebService.Authorization.Domain.Permission.Interface;
using WebService.Authorization.Domain.Permission.Model;
using WebService.Authorization.Domain.Role.Models;
using WebService.Authorization.Infrastructure.Repository.Role;
using WebService.Authorization.Infrastructure.Repository.RolePermission;
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

    public async Task UpdateAsync(PermissionEntity permissionEntity)
    {
        var sql = @"
                    UPDATE Permissions
	                SET
                        Code=@Code,
                        Name=@Name,
                        Enable=@Enable,
	                    LastModified=GETDATE(),
	                    LastModifiedBy=@LastModifiedBy
	                WHERE Id=@Id
                    ";
        using var connection = new SqlConnection(_dbConnectionOption.AuthorizationConnection);
        await connection.ExecuteAsync(sql, permissionEntity);
    }

    public async Task<PermissionEntity?> GetAsync(GetPermissionParameterModel parameterModel)
    {
        var sqlbuilder = new PermissionSqlBuilder<GetPermissionParameterModel>("SELECT * FROM Permissions WHERE 1=1", parameterModel)
            .QueryPermissionId()
            .QueryPermissionCode();
        var sql = sqlbuilder.BuildSql();
        var parameters = sqlbuilder.BuildParameters();
        using var conn = new SqlConnection(_dbConnectionOption.AuthorizationConnection);
        var result = await conn.QueryFirstOrDefaultAsync<PermissionEntity>(sql, parameters);
        return result;
    }

    public async Task<IEnumerable<PermissionEntity>?> GetListAsync(GetPermissionListParameterModel parameterModel)
    {
        var sqlbuilder = new PermissionSqlBuilder<GetPermissionListParameterModel>("SELECT * FROM Permissions WHERE 1=1", parameterModel)
            .QueryPermissionId();
        var sql = sqlbuilder.BuildSql();
        var parameters = sqlbuilder.BuildParameters();
        using var conn = new SqlConnection(_dbConnectionOption.AuthorizationConnection);
        var result = await conn.QueryAsync<PermissionEntity>(sql, parameters);
        return result;
    }
}