using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;

namespace Hx.MvcNavigation
{
    public interface INavigationService
    {
        void AddNavigationNode(NavigationItem item, bool appendToExisting = true);
        NavigationItem[] GetNavigationMap(string mapId = null, object modelData = null);
        NavigationItem[] GetNavigationForUser(IIdentity identity, string mapId, object modelData = null);
    }
}