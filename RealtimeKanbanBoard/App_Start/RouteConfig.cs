#region

using System.Web.Mvc;
using System.Web.Routing;

#endregion

namespace RealtimeKanbanBoard
{
    public class RouteConfig
    {
        /// <summary>
        ///     Registers the routes.
        /// </summary>
        /// <param name="routes">The routes.</param>
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute("User accounts", "Account/{action}/{id}", new
            {
                controller = "User",
                action = "Login",
                id = UrlParameter.Optional
            }
                );

            routes.MapRoute("Default", "default/{action}/{id}",
                new {controller = "Home", action = "index", id = UrlParameter.Optional}
                );
        }
    }
}