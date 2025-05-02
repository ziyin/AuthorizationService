using Dapper;
using System.Reflection;
using System.Text;

namespace WebService.Authorization.Infrastructure;

public class BaseSqlQueryBuilder<T>(string baseSql, T query)
{
    protected readonly StringBuilder _sqlBuilder = new(baseSql);
    protected readonly DynamicParameters _parameters = new();
    protected readonly T _query= query;
    public string BuildSql() => _sqlBuilder.ToString();
    public DynamicParameters BuildParameters() => _parameters;

    protected BaseSqlQueryBuilder<T> WhereIf(string condition, string paramName, object? value, bool likeEnable = false)
    {
        if (value is null || (value is string str && string.IsNullOrWhiteSpace(str)))
        {
            return this;
        }

        object val = value!;
        if (likeEnable && val is string s)
        {
            val = $"%{s}%";
        }
        _sqlBuilder.Append(" and " + condition);
        _parameters.Add(paramName, val);
        return this;
    }

    protected object? GetPropertyValue(string propertyName)
    {
        var prop = typeof(T).GetProperty(propertyName, BindingFlags.Public | BindingFlags.Instance);
        return prop?.GetValue(_query);
    }
}