using System;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.Web.Mvc;

namespace NavigationRoutes
{
    public static class NavigationRouteConfigurationExtensions
    {
        /// <summary>
        /// Maps the navigation route.
        /// </summary>
        /// <param name="routes">The routes.</param>
        /// <param name="name">The name.</param>
        /// <param name="url">The URL.</param>
        /// <param name="defaults">The defaults.</param>
        /// <param name="constraints">The constraints.</param>
        public static void MapNavigationRoute(this RouteCollection routes, string name, string url, object defaults,
                                              object constraints = null)
        {
            var newRoute = new NamedRoute(name, url, new MvcRouteHandler())
                {
                    Defaults = new RouteValueDictionary(defaults),
                    Constraints = new RouteValueDictionary(constraints)
                };
            routes.Add(name, newRoute);
        }

        /// <summary>
        /// Maps the navigation route.
        /// </summary>
        /// <param name="routes">The routes.</param>
        /// <param name="name">The name.</param>
        /// <param name="displayName">The display name.</param>
        /// <param name="url">The URL.</param>
        /// <param name="defaults">The defaults.</param>
        /// <param name="constraints">The constraints.</param>
        /// <returns>NavigationRouteBuilder.</returns>
        public static NavigationRouteBuilder MapNavigationRoute(this RouteCollection routes, string name,
                                                                string displayName, string url, object defaults,
                                                                object constraints = null)
        {
            var newRoute = new NamedRoute(name, displayName, url, new MvcRouteHandler())
                {
                    Defaults = new RouteValueDictionary(defaults),
                    Constraints = new RouteValueDictionary(constraints)
                };
            routes.Add(name, newRoute);
            return new NavigationRouteBuilder(routes, newRoute);
        }

        /// <summary>
        /// Maps the navigation route.
        /// </summary>
        /// <param name="routes">The routes.</param>
        /// <param name="name">The name.</param>
        /// <param name="displayName">The display name.</param>
        /// <param name="url">The URL.</param>
        /// <param name="defaults">The defaults.</param>
        /// <param name="namespaces">The namespaces.</param>
        /// <param name="constraints">The constraints.</param>
        public static void MapNavigationRoute(this RouteCollection routes, string name, string displayName, string url,
                                              object defaults,
                                              string[] namespaces,
                                              object constraints = null)
        {
            var newRoute = new NamedRoute(name, displayName, url, new MvcRouteHandler())
                {
                    Defaults = new RouteValueDictionary(defaults),
                    Constraints = new RouteValueDictionary(constraints),
                    DataTokens = new RouteValueDictionary()
                };

            if (namespaces != null && namespaces.Length > 0)
                newRoute.DataTokens["Namespaces"] = namespaces;

            routes.Add(name, newRoute);
        }


        /// <summary>
        /// Maps the navigation route.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="routes">The routes.</param>
        /// <param name="displayName">The display name.</param>
        /// <param name="action">The action.</param>
        /// <param name="areaName">Name of the area.</param>
        /// <returns>NavigationRouteBuilder.</returns>
        public static NavigationRouteBuilder MapNavigationRoute<T>(this RouteCollection routes, string displayName,
                                                                   Expression<Func<T, ActionResult>> action,
                                                                   string areaName = "") where T : IController
        {
            var newRoute = new NamedRoute("", "", new MvcRouteHandler());
            newRoute.ToDefaultAction(action, areaName);
            newRoute.DisplayName = displayName;
            routes.Add(newRoute.Name, newRoute);
            return new NavigationRouteBuilder(routes, newRoute);
        }

        /// <summary>
        /// Adds the child route.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="builder">The builder.</param>
        /// <param name="DisplayText">The display text.</param>
        /// <param name="action">The action.</param>
        /// <param name="areaName">Name of the area.</param>
        /// <returns>NavigationRouteBuilder.</returns>
        public static NavigationRouteBuilder AddChildRoute<T>(this NavigationRouteBuilder builder, string DisplayText,
                                                              Expression<Func<T, ActionResult>> action,
                                                              string areaName = "") where T : IController
        {
            var childRoute = new NamedRoute("", "", new MvcRouteHandler());
            childRoute.ToDefaultAction(action, areaName);
            childRoute.DisplayName = DisplayText;
            childRoute.IsChild = true;
            builder._parent.Children.Add(childRoute);
            builder._routes.Add(childRoute.Name, childRoute);
            return builder;
        }

        /// <summary>
        /// To the default action.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="route">The route.</param>
        /// <param name="action">The action.</param>
        /// <param name="areaName">Name of the area.</param>
        /// <returns>NamedRoute.</returns>
        /// <exception cref="System.ArgumentException">
        /// Expression must be a method call
        /// or
        /// Method call must target lambda argument
        /// </exception>
        public static NamedRoute ToDefaultAction<T>(this NamedRoute route, Expression<Func<T, ActionResult>> action,
                                                    string areaName) where T : IController
        {
            var body = action.Body as MethodCallExpression;

            if (body == null)
            {
                throw new ArgumentException("Expression must be a method call");
            }

            if (body.Object != action.Parameters[0])
            {
                throw new ArgumentException("Method call must target lambda argument");
            }

            string actionName = body.Method.Name;

            // check for ActionName attribute
            object[] attributes = body.Method.GetCustomAttributes(typeof (ActionNameAttribute), false);
            if (attributes.Length > 0)
            {
                var actionNameAttr = (ActionNameAttribute) attributes[0];
                actionName = actionNameAttr.Name;
            }

            string controllerName = typeof (T).Name;

            if (controllerName.EndsWith("Controller", StringComparison.OrdinalIgnoreCase))
            {
                controllerName = controllerName.Remove(controllerName.Length - 10, 10);
            }

            ;
            route.Defaults = LinkBuilder.BuildParameterValuesFromExpression(body) ?? new RouteValueDictionary();
            foreach (var pair in route.Defaults.Where(x => x.Value == null).ToList())
                route.Defaults.Remove(pair.Key);

            route.Defaults.Add("controller", controllerName);
            route.Defaults.Add("action", actionName);

            route.Url = CreateUrl(actionName, controllerName, areaName);
            //TODO: Add area to route name
            if (areaName == "")
                route.Name = "Navigation-" + controllerName + "-" + actionName;
            else
                route.Name = "Navigation-" + areaName + "-" + controllerName + "-" + actionName;

            if (route.DataTokens == null)
                route.DataTokens = new RouteValueDictionary();
            route.DataTokens.Add("Namespaces", new[] {typeof (T).Namespace});
            if (!string.IsNullOrEmpty(areaName))
            {
                route.DataTokens.Add("area", areaName.ToLower());
            }

            return route;
        }

        /// <summary>
        /// Creates the URL.
        /// </summary>
        /// <param name="actionName">Name of the action.</param>
        /// <param name="controllerName">Name of the controller.</param>
        /// <param name="areaName">Name of the area.</param>
        /// <returns>System.String.</returns>
        public static string CreateUrl(string actionName, string controllerName, string areaName)
        {
            string url = CreateUrl(actionName, controllerName);
            if (areaName == "")
                return url;

            return areaName.ToLower() + "/" + url;
        }

        /// <summary>
        /// Creates the URL.
        /// </summary>
        /// <param name="actionName">Name of the action.</param>
        /// <param name="controllerName">Name of the controller.</param>
        /// <returns>System.String.</returns>
        public static string CreateUrl(string actionName, string controllerName)
        {
            if (controllerName.Equals("home", StringComparison.CurrentCultureIgnoreCase))
            {
                if (actionName.Equals("index", StringComparison.CurrentCultureIgnoreCase))
                {
                    return "";
                }
                else
                {
                    return actionName.ToLower();
                }
            }

            return controllerName.ToLower() + "/" + actionName.ToLower();
        }
    }

    public class NavigationRouteBuilder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NavigationRouteBuilder"/> class.
        /// </summary>
        /// <param name="routes">The routes.</param>
        /// <param name="parent">The parent.</param>
        public NavigationRouteBuilder(RouteCollection routes, NamedRoute parent)
        {
            _routes = routes;
            _parent = parent;
        }

        /// <summary>
        /// Gets or sets the _routes.
        /// </summary>
        /// <value>The _routes.</value>
        public RouteCollection _routes { get; set; }

        /// <summary>
        /// Gets or sets the _parent.
        /// </summary>
        /// <value>The _parent.</value>
        public NamedRoute _parent { get; set; }
    }
}