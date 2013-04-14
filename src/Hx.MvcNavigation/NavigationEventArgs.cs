// ***********************************************************************
// Assembly         : Hx
// Author           : Ian Kulmatycki
// Created          : 04-13-2013
//
// Last Modified By : Ian Kulmatycki
// Last Modified On : 04-13-2013
// ***********************************************************************
// <copyright file="NavigationEventArgs.cs" company="HypothesisX">
//     Copyright (c) HypothesisX. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hx.MvcNavigation
{
    /// <summary>
    /// Class NavigationEventArgs
    /// </summary>
    public class NavigationEventArgs :EventArgs
    {
        /// <summary>
        /// Gets the item.
        /// </summary>
        /// <value>The item.</value>
        public NavigationItem Item { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="NavigationEventArgs"/> class.
        /// </summary>
        /// <param name="item">The item.</param>
        public NavigationEventArgs(NavigationItem item)
        {
            this.Item = item;
        }
    }
}
