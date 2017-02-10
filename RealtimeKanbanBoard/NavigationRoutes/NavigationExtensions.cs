using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;

namespace NavigationRoutes
{
    public class CompositeMvcHtmlString : IHtmlString
    {
        /// <summary>
        /// The _strings
        /// </summary>
        private readonly IEnumerable<IHtmlString> _strings;

        /// <summary>
        /// Initializes a new instance of the <see cref="CompositeMvcHtmlString"/> class.
        /// </summary>
        /// <param name="strings">The strings.</param>
        public CompositeMvcHtmlString(IEnumerable<IHtmlString> strings)
        {
            _strings = strings;
        }

        /// <summary>
        /// Returns an HTML-encoded string.
        /// </summary>
        /// <returns>An HTML-encoded string.</returns>
        public string ToHtmlString()
        {
            return string.Join(string.Empty, _strings.Select(x => x.ToHtmlString()));
        }
    }

    public static class NavigationRoutes
    {
        /// <summary>
        /// The filters
        /// </summary>
        public static List<INavigationRouteFilter> Filters = new List<INavigationRouteFilter>();
    }

    public static class NavigationViewExtensions
    {
        /// <summary>
        /// Navigations the specified helper.
        /// </summary>
        /// <param name="helper">The helper.</param>
        /// <returns>IHtmlString.</returns>
        public static IHtmlString Navigation(this HtmlHelper helper)
        {
            return new CompositeMvcHtmlString(
                GetRoutesForCurrentRequest(RouteTable.Routes, NavigationRoutes.Filters)
                    .Select(namedRoute => helper.NavigationListItemRouteLink(namedRoute)));
        }

        /// <summary>
        /// Gets the routes for current request.
        /// </summary>
        /// <param name="routes">The routes.</param>
        /// <param name="routeFilters">The route filters.</param>
        /// <returns>IEnumerable{NamedRoute}.</returns>
        public static IEnumerable<NamedRoute> GetRoutesForCurrentRequest(RouteCollection routes,
                                                                         IEnumerable<INavigationRouteFilter>
                                                                             routeFilters)
        {
            List<NamedRoute> navigationRoutes = routes.OfType<NamedRoute>().Where(r => r.IsChild == false).ToList();
            if (routeFilters.Count() > 0)
            {
                foreach (NamedRoute route in navigationRoutes.ToArray())
                {
                    foreach (INavigationRouteFilter filter in routeFilters)
                    {
                        if (filter.ShouldRemove(route))
                        {
                            navigationRoutes.Remove(route);
                            break;
                        }
                    }
                }
            }
            return navigationRoutes;
        }

        /// <summary>
        /// Navigations the list item route link.
        /// </summary>
        /// <param name="html">The HTML.</param>
        /// <param name="route">The route.</param>
        /// <returns>MvcHtmlString.</returns>
        public static MvcHtmlString NavigationListItemRouteLink(this HtmlHelper html, NamedRoute route)
        {
            var li = new TagBuilder("li")
                {
                    InnerHtml = html.RouteLink(route.DisplayName, route.Name).ToString()
                };

            if (CurrentRouteMatchesName(html, route.Name))
            {
                li.AddCssClass("active");
            }
            if (route.Children.Count() > 0)
            {
                //TODO: create a UL of child routes here.
                li.AddCssClass("dropdown");
                li.InnerHtml = "<a href=\"#\" class=\"dropdown-toggle\" data-toggle=\"dropdown\">" + route.DisplayName +
                               "<b class=\"caret\"></b></a>";
                var ul = new TagBuilder("ul");
                ul.AddCssClass("dropdown-menu");

                foreach (NamedRoute child in route.Children)
                {
                    var childLi = new TagBuilder("li");
                    childLi.InnerHtml = html.RouteLink(child.DisplayName, child.Name).ToString();
                    ul.InnerHtml += childLi.ToString();
                }
                //that would mean we need to make some quick

                li.InnerHtml = "<a href='#' class='dropdown-toggle' data-toggle='dropdown'>" + route.DisplayName +
                               " <b class='caret'></b></a>" + ul;
            }
            return MvcHtmlString.Create(li.ToString(TagRenderMode.Normal));
        }

        /// <summary>
        /// Currents the name of the route matches.
        /// </summary>
        /// <param name="html">The HTML.</param>
        /// <param name="routeName">Name of the route.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        private static bool CurrentRouteMatchesName(HtmlHelper html, string routeName)
        {
            var namedRoute = html.ViewContext.RouteData.Route as NamedRoute;
            if (namedRoute != null)
            {
                if (string.Equals(routeName, namedRoute.Name, StringComparison.InvariantCultureIgnoreCase))
                {
                    return true;
                }
            }
            return false;
        }
    }
}