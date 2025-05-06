namespace WebService.Authorization.Infrastructure.Repository.RolePermission;

public class RolePermissionSqlBuilder<T>
    (
    string baseSql,
    T query
    ) : BaseSqlQueryBuilder<T>(baseSql, query)
{
    public RolePermissionSqlBuilder<T> QueryRoleId()
    {
        var value = GetPropertyValue("RoleId");
        return (RolePermissionSqlBuilder<T>)WhereIf("RoleId = @RoleId", "RoleId", value);
    }

    public RolePermissionSqlBuilder<T> QueryPermissionId()
    {
        var value = GetPropertyValue("PermissionId");
        return (RolePermissionSqlBuilder<T>)WhereIf("PermissionId = @PermissionId", "PermissionId", value);
    }

    public RolePermissionSqlBuilder<T> QueryCode()
    {
        var value = GetPropertyValue("Code");
        return (RolePermissionSqlBuilder<T>)WhereIf("Code = @Code", "Code", value);
    }
}