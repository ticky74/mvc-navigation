// ***********************************************************************
// Assembly         : Hx.MvcNavigation
// Author           : Ian Kulmatycki
// Created          : 04-13-2013
//
// Last Modified By : Ian Kulmatycki
// Last Modified On : 04-14-2013
// ***********************************************************************
// <copyright file="MvcNavigationAction.cs" company="HypothesisX">
//     Copyright (c) HypothesisX. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Web.Routing;

namespace Hx.MvcNavigation
{
    /// <summary>
    /// Class MvcNavigationAction
    /// </summary>
    public class MvcNavigationAction : INavigationLocation
    {
        /// <summary>
        /// Gets or sets the name of the controller.
        /// </summary>
        /// <value>The name of the controller.</value>
        public string ControllerName { get; set; }
        /// <summary>
        /// Gets or sets the controller action.
        /// </summary>
        /// <value>The controller action.</value>
        public string ControllerAction { get; set; }
        /// <summary>
        /// Gets or sets the name of the route.
        /// </summary>
        /// <value>The name of the route.</value>
        public string RouteName { get; set; }


        // TODO pass more values
        /// <summary>
        /// Writes the location URL.
        /// </summary>
        /// <param name="principal">The principal.</param>
        /// <param name="context">The context.</param>
        /// <returns>System.String.</returns>
        public string WriteLocationUrl(IPrincipal principal, RequestContext context)
        {
            if (this.OnActionGenerating != null)
            {
                this.OnActionGenerating(principal, context);
            }
            string url = System.Web.Mvc.UrlHelper.GenerateUrl(this.RouteName, this.ControllerAction, this.ControllerName, context.RouteData.Values, RouteTable.Routes,
                                context, false);

            return url;
        }


        /// <summary>
        /// Sets the on action generating.
        /// </summary>
        /// <value>The on action generating.</value>
        public Action<IPrincipal, RequestContext> OnActionGenerating
        {
            set;
            private get;
        }
    }
}
