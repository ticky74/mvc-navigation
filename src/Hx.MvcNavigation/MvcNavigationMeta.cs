using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hx.MvcNavigation
{
    public class MvcNavigationMeta : INavigationMeta
    {
        #region Properties
        public string Description { get; set; }

        public int PreferredOrder { get; set; }

        public string Text { get; set; }

        public AuthorizationType AccessType { get; set; }

        public string[] Roles { get; set; }
        #endregion Properties

        #region Constructors
        public MvcNavigationMeta()
        {
            this.AccessType = AuthorizationType.Grant;
        }
        #endregion Constructors
    }
}
