using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;

namespace Hx.MvcNavigation
{
    public class RoleProviderSecurityService : IAuthorizationService
    {
        public string[] GetRolesForUser(string username)
        {
            return Roles.GetRolesForUser(username);
        }
    }
}
