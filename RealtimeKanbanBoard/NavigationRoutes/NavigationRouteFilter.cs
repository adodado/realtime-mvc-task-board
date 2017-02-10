using System.Web.Routing;

namespace NavigationRoutes
{
    public class NavigationRouteFilter : INavigationRouteFilter
    {
        /// <summary>
        /// Shoulds the remove.
        /// </summary>
        /// <param name="route">The route.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool ShouldRemove(Route route)
        {
            return true;
        }
    }
}