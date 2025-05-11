using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebService.Authorization.Domain.UserRole.Interface;

public interface IBindUserToRoleManager
{
    Task<IEnumerable<Guid>?> GetCanBindRolesAsync(Guid userId, IEnumerable<Guid> roles);
}