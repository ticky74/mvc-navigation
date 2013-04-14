using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Hx.MvcNavigation
{
    public class NavigationItem
    {
        #region Constants and Variables
        #endregion Constants and Variables

        #region Properties
        public IList<NavigationItem> Children { get; set; }
        internal bool IsPlaceHolderItem { get; set; }
        public string ItemIdentifier { get; set; }
        public string ItemParentIdentifier { get; set; }
        public INavigationLocation Location { get; set; }
        public INavigationMeta Meta { get; set; }
        public Action<NavigationItem, object> OnRenderItem { internal get; set; }
        public Action<NavigationItem> OnIsSelectedItem { internal get; set; }
        public Dictionary<string, object> Styles { get; set; }
        public dynamic UserData { get; set; }
        #endregion

        #region Constructors
        public NavigationItem()
        {
            this.Children = new List<NavigationItem>();
        }
        #endregion Constructors

        #region Methods
        internal NavigationItem ToAuthenticatedTree(string[] roles)
        {
            roles = roles??new string[0]; // Must be something better than this?..

            if (this.Meta.AccessType == AuthorizationType.Public
                    || (this.Meta.Roles.Intersect(roles).Any() == (this.Meta.AccessType == AuthorizationType.Grant) ? true : false))
            {
                NavigationItem item = new NavigationItem
                {
                    IsPlaceHolderItem = this.IsPlaceHolderItem,
                    ItemIdentifier = this.ItemIdentifier,
                    ItemParentIdentifier = this.ItemParentIdentifier,
                    OnIsSelectedItem = this.OnIsSelectedItem,
                    OnRenderItem = this.OnRenderItem,
                    UserData = this.UserData,
                    Location = this.Location,
                    Meta = this.Meta,
                    Styles = this.Styles
                };

                    List<NavigationItem> c = new List<NavigationItem>();
                if (this.Children != null && this.Children.Count() > 0)
                {
                    foreach (var child in this.Children.OrderBy(x => x.Meta.PreferredOrder))
                    {
                        var tree = child.ToAuthenticatedTree(roles);
                        if (tree != null)
                        {
                            c.Add(tree);
                        }

                    }
                    c.Sort(NavigationItemSortComparer.Instance);
                    
                }
                item.Children = c;
                return item;
            }
            else
            {
                return null;
            }

            //items.Where(template => (template.Meta.AccessType == AuthorizationType.Public
            //            || (template.Meta.Roles.Intersect(userRoles).Any() == (template.Meta.AccessType == AuthorizationType.Grant) ? true : false)))
            //            .Select(template => template.ToAuthenticatedTree()).ToList();

        }
        internal void TransitionFromPlaceholder(NavigationItem item)
        {
            if (this.IsPlaceHolderItem)
            {
                this.Meta = item.Meta;
                this.Location = item.Location;
                this.OnRenderItem = item.OnRenderItem;
                this.OnIsSelectedItem = item.OnIsSelectedItem;
                this.Styles = item.Styles;
                this.UserData = item.UserData;
                this.IsPlaceHolderItem = false;

                // Action<IEnumerable<Item>> Recurse = null;
            }
        }
        #endregion Methods
    }
}
