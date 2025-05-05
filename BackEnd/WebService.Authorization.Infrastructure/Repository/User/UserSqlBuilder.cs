namespace WebService.Authorization.Infrastructure.Repository.User;

public class UserSqlBuilder<T>
    (
    string baseSql,
    T query
    ) : BaseSqlQueryBuilder<T>(baseSql, query)
{
    public UserSqlBuilder<T> QueryName()
    {
        var value = GetPropertyValue("Name");
        return (UserSqlBuilder<T>)WhereIf("Name = @Name", "Name", value);
    }

    public UserSqlBuilder<T> QueryUserId()
    {
        var value = GetPropertyValue("UserId");
        return (UserSqlBuilder<T>)WhereIf("Id = @Id", "Id", value);
    }

    public UserSqlBuilder<T> QueryAccount()
    {
        var value = GetPropertyValue("Account");
        return (UserSqlBuilder<T>)WhereIf("Account = @Account", "Account", value);
    }

    public UserSqlBuilder<T> QueryRegion()
    {
        var value = GetPropertyValue("RegionBusinessUnit");
        return (UserSqlBuilder<T>)WhereIf("RegionBusinessUnit = @RegionBusinessUnit", "RegionBusinessUnit", value);
    }

    public UserSqlBuilder<T> QueryEnable()
    {
        var value = GetPropertyValue("Enable");
        return (UserSqlBuilder<T>)WhereIf("Enable = @Enable", "Enable", value);
    }
}