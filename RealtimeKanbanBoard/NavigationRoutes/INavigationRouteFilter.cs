using System.Web.Routing;

namespace NavigationRoutes
{
    /// <summary>
    /// Interface INavigationRouteFilter
    /// </summary>
    public interface INavigationRouteFilter
    {
        /// <summary>
        /// Shoulds the remove.
        /// </summary>
        /// <param name="navigationRoutes">The navigation routes.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        bool ShouldRemove(Route navigationRoutes);
    }
}