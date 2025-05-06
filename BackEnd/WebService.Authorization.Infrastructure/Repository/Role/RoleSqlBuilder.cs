namespace WebService.Authorization.Infrastructure.Repository.Role;

public class RoleSqlBuilder<T>
    (
    string baseSql,
    T query
    ) : BaseSqlQueryBuilder<T>(baseSql, query)
{
    public RoleSqlBuilder<T> QueryRoleName()
    {
        var value = GetPropertyValue("RoleName");
        if (value is IEnumerable<string> names && names.Any())
        {
            _sqlBuilder.Append(" AND Name IN @Names");
            _parameters.Add("Names", names);
            return this;
        }
        return (RoleSqlBuilder<T>)WhereIf("Name = @Name", "Name", value);
    }

    public RoleSqlBuilder<T> QueryRoleId()
    {
        var value = GetPropertyValue("RoleId");
        if (value is IEnumerable<Guid> ids && ids.Any())
        {
            _sqlBuilder.Append(" AND Id IN @Ids");
            _parameters.Add("Ids", ids);
            return this;
        }
        return (RoleSqlBuilder<T>)WhereIf("Id = @Id", "Id", value);
    }
}