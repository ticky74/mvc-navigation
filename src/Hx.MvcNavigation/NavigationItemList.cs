using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hx.MvcNavigation
{
    [Obsolete("This was a not to smart idea... I think", true)]
    public class NavigationItemList : List<NavigationItem>
    {
        #region Events
        public event EventHandler<NavigationEventArgs> ItemAdded;
        #endregion Events

        public new void Add(NavigationItem item)
        {
            this.Add(item);
            if (this.ItemAdded != null)
            {
                this.ItemAdded(this, new NavigationEventArgs(item));
            }
        }
    }
}
