namespace WebService.Authorization.Infrastructure.Repository.Permission;

public class PermissionSqlBuilder<T>
    (
    string baseSql,
    T query
    ) : BaseSqlQueryBuilder<T>(baseSql, query)
{
    public PermissionSqlBuilder<T> QueryPermissionCode()
    {
        var value = GetPropertyValue("PermissionCode");
        return (PermissionSqlBuilder<T>)WhereIf("Code = @Code", "Code", value);
    }

    public PermissionSqlBuilder<T> QueryPermissionId()
    {
        var value = GetPropertyValue("PermissionId");
        if (value is IEnumerable<Guid> ids && ids.Any())
        {
            _sqlBuilder.Append(" AND Id IN @Ids");
            _parameters.Add("Ids", ids);
            return this;
        }
        return (PermissionSqlBuilder<T>)WhereIf("Id = @Id", "Id", value);
    }
}