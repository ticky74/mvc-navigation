using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Web.Routing;

namespace Hx.MvcNavigation
{
    public interface INavigationLocation
    {
        string WriteLocationUrl(IPrincipal principal, RequestContext context);
        Action<IPrincipal, RequestContext> OnActionGenerating { set; }
    }
}
