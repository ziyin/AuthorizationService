namespace WebService.Authorization.Infrastructure.Repository.UserRole;

public class UserRoleSqlBuilder<T>
    (
    string baseSql, 
    T query
    ) : BaseSqlQueryBuilder<T>(baseSql, query)
{
    public UserRoleSqlBuilder<T> QueryUserId()
    {
        var value = GetPropertyValue("UserId");
        return (UserRoleSqlBuilder<T>)WhereIf("UserId = @UserId", "UserId", value);
    }

    public UserRoleSqlBuilder<T> QueryRoleId()
    {
        var value = GetPropertyValue("RoleId");
        return (UserRoleSqlBuilder<T>)WhereIf("RoleId = @RoleId", "RoleId", value);
    }
}