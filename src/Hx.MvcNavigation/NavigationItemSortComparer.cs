using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hx.MvcNavigation
{
    public class NavigationItemSortComparer : IComparer<NavigationItem>
    {
        public readonly static NavigationItemSortComparer Instance = new NavigationItemSortComparer();

        public int Compare(NavigationItem x, NavigationItem y)
        {
            if (x == null && y == null) return 0;
            if (y == null) return 1;
            if (x == null) return -1;

            if (x.Meta == null && y.Meta == null) return 0;
            if (x.Meta == null) return -1;
            if (y.Meta == null) return 1;

            if (x.Meta.PreferredOrder == y.Meta.PreferredOrder) return 0;
            return (x.Meta.PreferredOrder > y.Meta.PreferredOrder) ? 1 : -1;
        }
    }
}
