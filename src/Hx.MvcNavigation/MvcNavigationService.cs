using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;

namespace Hx.MvcNavigation
{
    public class MvcNavigationService : INavigationService
    {
        #region Constants and Variables
        private readonly IAuthorizationService authorizationService;
        private readonly Dictionary<string, NavigationItem> items = new Dictionary<string, NavigationItem>();
        #endregion Constants and Variables

        #region Constructors
        public MvcNavigationService(IAuthorizationService authorizationService)
        {
            this.authorizationService = authorizationService;
        }
        #endregion Constructors

        public void AddNavigationNode(NavigationItem item, bool appendToExisting = true)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(item.ItemParentIdentifier))
                {
                    // Root item
                    if (items.ContainsKey(item.ItemIdentifier))
                        throw new InvalidOperationException(string.Format("Specified key already exists in the navigation map. ({0})", item.ItemIdentifier));

                    items[item.ItemIdentifier] = item;
                }
                else
                {
                    // 1. Check to see if the item was added earlier as a placeholder
                    var matchingPlaceholder = this.items.Values.Where(x => x.ItemIdentifier == item.ItemIdentifier).SingleOrDefault();
                    if (matchingPlaceholder != null)
                    {
                        if (!matchingPlaceholder.IsPlaceHolderItem)
                        {
                            throw new InvalidOperationException(string.Format("Specified key already exists in the navigation map. ({0})", item.ItemIdentifier));
                        }

                        matchingPlaceholder.TransitionFromPlaceholder(item);
                    }

                    // Look for parent, if none found create a dummy placeholder item that can be
                    // replaced later.
                    var parent = items[item.ItemParentIdentifier];
                    if (parent == null)
                    {
                        parent = new NavigationItem
                        {
                            IsPlaceHolderItem = true,
                            ItemIdentifier = item.ItemParentIdentifier
                        };
                        items.Add(parent.ItemIdentifier, parent);
                    }

                    parent.Children.Add(item);
                }
            }
            catch (NullReferenceException nre)
            {
                // This exception will rarely be thrown so it is more efficient
                // to throw 1 in a million than check every single time
                // them method is called
                throw new ArgumentNullException("Navigation Item cannot be null", nre);
            }

            //if (!maps.ContainsKey(item.MapId))
            //{
            //    maps.Add(item.MapId, new ActionList());
            //}

            //maps[item.MapId].Add(item);
        }

        public NavigationItem[] GetNavigationMap(string mapId = null, object modelData = null)
        {
            if (string.IsNullOrWhiteSpace(mapId))
            {
                // Return all of the root items
                return this.items.Values.Where(x => string.IsNullOrWhiteSpace(x.ItemParentIdentifier)).ToArray();
            }
            else
            {
                return new[]{ this.items[mapId] };
                     //.Values.Where(x => string.Compare(mapId, x.ItemIdentifier, false) == 0).ToArray();
            }
        }

        public NavigationItem[] GetNavigationForUser(System.Security.Principal.IIdentity identity, string mapId = null, object modelData = null)
        {
            var items = this.GetNavigationMap(mapId);
            if (items.Length == 0)
                return items;

            string[] userRoles = this.authorizationService.GetRolesForUser(identity.Name);
            var userNavItems = items.Where(template => (template.Meta.AccessType == AuthorizationType.Public
                        || (template.Meta.Roles.Intersect(userRoles).Any() == (template.Meta.AccessType == AuthorizationType.Grant) ? true : false)))
                        .Select(template => template.ToAuthenticatedTree(userRoles)).OrderBy(x => x.Meta.PreferredOrder).ToArray();

            return userNavItems;
        }
    }
}
