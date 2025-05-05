using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using WebService.Authorization.Domain.Role.Interfaces;
using WebService.Authorization.Domain.Role.Models;
using WebService.Authorization.Shard.Options;

namespace WebService.Authorization.Infrastructure.Repository.Role;

public class RoleRepository
    (
    IOptions<DbConnectionOption> options
    ) : IRoleRepository
{
    private readonly DbConnectionOption _dbConnectionOption = options.Value;

    public async Task<Guid> CreateAsync(RoleEntity roleEntity)
    {
        var sql = @"
                    INSERT INTO Roles
                    (
                        Name, 
                        CreateTime,
                        Creator, 
                        Enable
                    )
                    OUTPUT INSERTED.Id
                    VALUES
                    (
                        @Name,
                        GETDATE(), 
                        @Creator, 
                        @Enable
                    );
                    ";

        using var connection = new SqlConnection(_dbConnectionOption.AuthorizationConnection);

        var insertedId = await connection.ExecuteScalarAsync<Guid>(sql, roleEntity);

        return insertedId;
    }

    public async Task<RoleEntity?> GetAsync(GetRoleParameterModel parameterModel)
    {
        var sqlbuilder = new RoleSqlBuilder<GetRoleParameterModel>("SELECT * FROM Roles WHERE 1=1", parameterModel)
            .QueryRoleId()
            .QueryName();
        var sql = sqlbuilder.BuildSql();
        var parameters = sqlbuilder.BuildParameters();
        using var conn = new SqlConnection(_dbConnectionOption.AuthorizationConnection);
        var result = await conn.QueryFirstOrDefaultAsync<RoleEntity>(sql, parameters);
        return result;
    }

    public async Task<IEnumerable<RoleEntity>?> GetListAsync(GetRoleListParameterModel parameterModel)
    {
        var sqlbuilder = new RoleSqlBuilder<GetRoleListParameterModel>("SELECT * FROM Roles WHERE 1=1", parameterModel)
            .QueryRoleIds();
        var sql = sqlbuilder.BuildSql();
        var parameters = sqlbuilder.BuildParameters();
        using var conn = new SqlConnection(_dbConnectionOption.AuthorizationConnection);
        var result = await conn.QueryAsync<RoleEntity>(sql, parameters);
        return result;
    }
}