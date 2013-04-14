using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hx.MvcNavigation
{
    public interface INavigationMeta
    {
        AuthorizationType AccessType { get; set; }
        string Description { get; set; }
        int PreferredOrder { get; set; }
        string[] Roles { get; set; }
        string Text { get; set; }
    }
}
