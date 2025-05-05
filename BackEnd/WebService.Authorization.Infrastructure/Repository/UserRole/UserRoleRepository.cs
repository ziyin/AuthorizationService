using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using WebService.Authorization.Domain.User.Models.Parameters;
using WebService.Authorization.Domain.User.Models;
using WebService.Authorization.Domain.UserRole.Interface;
using WebService.Authorization.Domain.UserRole.Model;
using WebService.Authorization.Domain.UserRole.Model.Parameter;
using WebService.Authorization.Infrastructure.Repository.User;
using WebService.Authorization.Shard.Options;

namespace WebService.Authorization.Infrastructure.Repository.UserRole;

public class UserRoleRepository
    (
        IOptions<DbConnectionOption> options
    ) : IUserRoleRepository
{
    private readonly DbConnectionOption _dbConnectionOption = options.Value;

    public async Task CreateAsync(UserRoleEntity userRoleEntity)
    {
        var sql = @"
                    INSERT INTO UserRoles
                    (
                        UserId, 
                        RoleId,
                        Creator,
                        CreateTime
                    )
                    VALUES
                    (
                        @UserId,
                        @RoleId, 
                        @Creator, 
                        GETDATE()
                    )
                   ";

        using var connection = new SqlConnection(_dbConnectionOption.AuthorizationConnection);
        await connection.ExecuteAsync(sql, userRoleEntity);
    }

    public async Task CreateManyAsync(IEnumerable<UserRoleEntity> userRoleEntities)
    {
        var sql = @"
                    INSERT INTO UserRoles
                    (
                        UserId, 
                        RoleId,
                        Creator,
                        CreateTime
                    )
                    VALUES
                    (
                        @UserId,
                        @RoleId, 
                        @Creator, 
                        GETDATE()
                    )
                   ";

        using var connection = new SqlConnection(_dbConnectionOption.AuthorizationConnection);
        await connection.ExecuteAsync(sql, userRoleEntities);
    }

    public async Task<IEnumerable<UserRoleDataModel>?> GetListAsync(GetUserRoleListParameterModel parameterModel)
    {
        var sqlBaseCommand = @"SELECT 
                                	UR.Id,
                                	UR.UserId,
                                	UR.RoleId,
                                	Roles.[Name] as RoleName,
                                	UR.CreateTime,
                                	UR.Creator
                                FROM UserRoles UR WITH(NOLOCK)
                                JOIN Roles on Roles.Id=UR.RoleId
                                JOIN Users on Users.Id=UR.UserId
                                WHERE Roles.[Enable]=1 AND Users.[Enable]=1 ";

        var sqlbuilder = new UserRoleSqlBuilder<GetUserRoleListParameterModel>(sqlBaseCommand, parameterModel)
            .QueryUserId()
            .QueryRoleId();
        var sql = sqlbuilder.BuildSql();
        var parameters = sqlbuilder.BuildParameters();
        using var conn = new SqlConnection(_dbConnectionOption.AuthorizationConnection);
        var result = await conn.QueryAsync<UserRoleDataModel>(sql, parameters);
        return result;
    }
}