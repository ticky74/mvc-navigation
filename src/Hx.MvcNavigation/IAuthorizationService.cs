using System.Web.Security;

namespace Hx.MvcNavigation
{
    public interface IAuthorizationService
    {
        string[] GetRolesForUser(string username);
    }
}