using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebService.Authorization.Domain.User.Models.Parameters;

namespace WebService.Authorization.Infrastructure.Repository;

public class UserSqlBuilder : BaseSqlQueryBuilder<GetUserListParameterModel>
{
    public UserSqlBuilder(string baseSql, GetUserListParameterModel query) : base(baseSql, query) { }

    public UserSqlBuilder QueryName()
    {
        var value = GetPropertyValue("Name");
        return (UserSqlBuilder)WhereIf("Name = @Name", "Name", value);
    }

    public UserSqlBuilder QueryAccount()
    {
        var value = GetPropertyValue("Account");
        return (UserSqlBuilder)WhereIf("Account = @Account", "Account", value);
    }

    public UserSqlBuilder QueryRegion()
    {
        var value = GetPropertyValue("RegionBusinessUnit");
        return (UserSqlBuilder)WhereIf("RegionBusinessUnit = @RegionBusinessUnit", "RegionBusinessUnit", value);
    }
}