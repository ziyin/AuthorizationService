namespace WebService.Authorization.Infrastructure.Repository.Role;

public class RoleSqlBuilder<T>
    (
    string baseSql,
    T query
    ) : BaseSqlQueryBuilder<T>(baseSql, query)
{
    public RoleSqlBuilder<T> QueryName()
    {
        var value = GetPropertyValue("Name");
        return (RoleSqlBuilder<T>)WhereIf("Name = @Name", "Name", value);
    }

    public RoleSqlBuilder<T> QueryRoleId()
    {
        var value = GetPropertyValue("RoleId");
        return (RoleSqlBuilder<T>)WhereIf("Id = @Id", "Id", value);
    }
}