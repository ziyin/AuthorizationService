using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System.Text;
using WebService.Authorization.Domain.User.Interfaces;
using WebService.Authorization.Domain.User.Models;
using WebService.Authorization.Domain.User.Models.Parameters;
using WebService.Authorization.Infrastructure.Options;

namespace WebService.Authorization.Infrastructure.Repository;

public class UserRepository
    (
    IOptions<DbConnectionOption> options
    ) : IUserRepository
{
    private readonly DbConnectionOption _dbConnectionOption = options.Value;

    public async Task<Guid> CreateAsync(CreateUserParameterModel parameterModel)
    {
        var sqlCommand = new StringBuilder();
        sqlCommand.Append("insert into Users");
        sqlCommand.Append("( ");
        sqlCommand.Append("Name, Account, Password, RegionBusinessUnit, Email, Phone, Address, CreateTime, Creator");
        sqlCommand.Append(" )");
        sqlCommand.Append("OUTPUT INSERTED.Id ");
        sqlCommand.Append("values");
        sqlCommand.Append("( ");
        sqlCommand.Append("@Name, @Account, @Password, @RegionBusinessUnit, @Email, @Phone, @Address, GETDATE(), @Creator");
        sqlCommand.Append(" )");
        using var connection = new SqlConnection(_dbConnectionOption.AuthorizationConnection);
        var insertedId = await connection.ExecuteScalarAsync<Guid>(sqlCommand.ToString(), parameterModel);
        return insertedId;
    }

    public async Task<UserDataModel?> GetAsync(Guid userId)
    {
        var sqlCommand = new StringBuilder();
        sqlCommand.Append("select ");
        sqlCommand.Append("Id, Name, Account, Password, RegionBusinessUnit, Email, Phone, Address, CreateTime, Creator");
        sqlCommand.Append("from Users with(nolock) ");
        sqlCommand.Append("where Id=@userId");
        using var connection = new SqlConnection(_dbConnectionOption.AuthorizationConnection);
        var dynamicParameter = new DynamicParameters();
        dynamicParameter.Add("userId", userId);
        var result = await connection.QueryFirstOrDefaultAsync<UserDataModel>(sqlCommand.ToString(), dynamicParameter);
        return result;
    }

    public async Task<IEnumerable<UserDataModel>?> GetListAsync(GetUserListParameterModel parameterModel)
    {
        var sqlbuilder = new UserSqlBuilder("SELECT * FROM Users WHERE 1=1", parameterModel)
            .QueryName()
            .QueryAccount()
            .QueryRegion();
        var sql = sqlbuilder.BuildSql();
        var parameters = sqlbuilder.BuildParameters();
        using var conn = new SqlConnection(_dbConnectionOption.AuthorizationConnection);
        var result = await conn.QueryAsync<UserDataModel>(sql, parameters);
        return result;
    }
}