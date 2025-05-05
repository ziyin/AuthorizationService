using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System.Text;
using WebService.Authorization.Domain.User.Interfaces;
using WebService.Authorization.Domain.User.Models;
using WebService.Authorization.Domain.User.Models.Parameters;
using WebService.Authorization.Shard.Options;

namespace WebService.Authorization.Infrastructure.Repository.User;

public class UserRepository
    (
    IOptions<DbConnectionOption> options
    ) : IUserRepository
{
    private readonly DbConnectionOption _dbConnectionOption = options.Value;

    public async Task<Guid> CreateAsync(UserEntity userEntity)
    {
        var sql = @"
                    INSERT INTO Users (
                        Name,
                        Account,
                        Password,
                        RegionBusinessUnit,
                        Email,
                        Phone,
                        Address,
                        CreateTime,
                        Creator,
                        Enable
                    )
                    OUTPUT INSERTED.Id
                    VALUES (
                        @Name,
                        @Account,
                        @Password,
                        @RegionBusinessUnit,
                        @Email,
                        @Phone,
                        @Address,
                        GETDATE(),
                        @Creator,
                        @Enable
                    );";

        using var connection = new SqlConnection(_dbConnectionOption.AuthorizationConnection);

        var insertedId = await connection.ExecuteScalarAsync<Guid>(sql, userEntity);

        return insertedId;
    }

    public async Task UpdateAsync(UserEntity userEntity)
    {
        var sql = @"
                    UPDATE Users
                    SET 
                        Name = @Name,
                        Password = @Password,
                        RegionBusinessUnit = @RegionBusinessUnit,
                        Email = @Email,
                        Phone = @Phone,
                        Address = @Address,
                        Enable = @Enable,
                        LastModified = GETDATE(),
                        LastModifiedBy = @LastModifiedBy
                    WHERE Id = @Id;
                    ";
        using var connection = new SqlConnection(_dbConnectionOption.AuthorizationConnection);
        await connection.ExecuteAsync(sql, userEntity);
    }

    public async Task<UserEntity?> GetAsync(GetUserParameterModel parameterModel)
    {
        var sqlbuilder = new UserSqlBuilder<GetUserParameterModel>("SELECT * FROM Users WHERE 1=1", parameterModel)
            .QueryUserId()
            .QueryAccount();
        var sql = sqlbuilder.BuildSql();
        var parameters = sqlbuilder.BuildParameters();
        using var conn = new SqlConnection(_dbConnectionOption.AuthorizationConnection);
        var result = await conn.QueryFirstOrDefaultAsync<UserEntity>(sql, parameters);
        return result;
    }

    public async Task<IEnumerable<UserEntity>?> GetListAsync(GetUserListParameterModel parameterModel)
    {
        var sqlbuilder = new UserSqlBuilder<GetUserListParameterModel>("SELECT * FROM Users WHERE 1=1", parameterModel)
            .QueryName()
            .QueryAccount()
            .QueryRegion()
            .QueryEnable();
        var sql = sqlbuilder.BuildSql();
        var parameters = sqlbuilder.BuildParameters();
        using var conn = new SqlConnection(_dbConnectionOption.AuthorizationConnection);
        var result = await conn.QueryAsync<UserEntity>(sql, parameters);
        return result;
    }
}