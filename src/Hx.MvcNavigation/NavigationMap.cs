using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
//using System.Web.Security;

namespace Hx.MvcNavigation
{
    //public class NavigationMap : INavigationService
    //{
    //    #region Constants and Variables
    //    private readonly Dictionary<string, ActionList> _maps = new Dictionary<string, ActionList>();
    //    #endregion

    //    #region Methods
    //    public void AddNavigationNode(NavigationItem item)
    //    {
    //        if (string.IsNullOrWhiteSpace(item.MapId)) throw new ArgumentNullException("map");

    //        if (!_maps.ContainsKey(item.MapId))
    //        {
    //            _maps.Add(item.MapId, new ActionList());
    //        }

    //        _maps[item.MapId].Add(item);
    //    }

        

    //    public NavigationItem[] GetNavigationForUser(IIdentity identity, string mapId, object modelData = null)
    //    {
    //        ActionList list = null;
    //        _maps.TryGetValue(mapId, out list);
    //        if (list != null)
    //        {
    //            string[] userRoles = Roles.GetRolesForUser(identity.Name);
    //            var results = list.Where(x => x.Roles.Intersect(userRoles).Any() == (x.AccessType == AuthorizationType.Grant)?true:false)
    //                .OrderBy(x => x.PreferredOrder).ThenBy(x => x.Text).ToArray();
    //            foreach (var node in results)
    //            {
    //                if (node.OnRenderItem != null)
    //                {
    //                    node.OnRenderItem(node, modelData);
    //                }
    //            }

    //            return results;
    //        }
    //        else
    //        {
    //            return new NavigationItem[0];
    //        }
    //    }
    //    #endregion


    //    public NavigationItem[] GetNavigationHubForUser(IIdentity identity, string groupId, object modelData = null)
    //    {
    //        var groups = this.GetNavigationForUser(identity, groupId, modelData);

    //        foreach (var g in groups)
    //        {
    //            g.Children = this.GetNavigationForUser(identity, g.ItemId, modelData);
    //        }

    //        return groups;
    //    }
    //}
}
