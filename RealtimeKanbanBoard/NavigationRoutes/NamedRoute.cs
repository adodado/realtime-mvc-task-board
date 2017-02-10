using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Routing;

namespace NavigationRoutes
{
    public class NamedRoute : Route
    {
        /// <summary>
        /// The _child routes
        /// </summary>
        private readonly List<NamedRoute> _childRoutes = new List<NamedRoute>();
        /// <summary>
        /// The _display name
        /// </summary>
        private string _displayName;
        /// <summary>
        /// The _name
        /// </summary>
        private string _name;

        /// <summary>
        /// Initializes a new instance of the <see cref="NamedRoute"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="url">The URL.</param>
        /// <param name="routeHandler">The route handler.</param>
        public NamedRoute(string name, string url, IRouteHandler routeHandler)
            : base(url, routeHandler)
        {
            _name = name;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NamedRoute"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="url">The URL.</param>
        /// <param name="defaults">The defaults.</param>
        /// <param name="constraints">The constraints.</param>
        /// <param name="routeHandler">The route handler.</param>
        public NamedRoute(string name, string url, RouteValueDictionary defaults, RouteValueDictionary constraints,
                          IRouteHandler routeHandler)
            : base(url, defaults, constraints, routeHandler)
        {
            _name = name;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NamedRoute"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="url">The URL.</param>
        /// <param name="defaults">The defaults.</param>
        /// <param name="constraints">The constraints.</param>
        /// <param name="dataTokens">The data tokens.</param>
        /// <param name="routeHandler">The route handler.</param>
        public NamedRoute(string name, string url, RouteValueDictionary defaults, RouteValueDictionary constraints,
                          RouteValueDictionary dataTokens, IRouteHandler routeHandler)
            : base(url, defaults, constraints, dataTokens, routeHandler)
        {
            _name = name;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NamedRoute"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="displayName">The display name.</param>
        /// <param name="url">The URL.</param>
        /// <param name="defaults">The defaults.</param>
        /// <param name="constraints">The constraints.</param>
        /// <param name="dataTokens">The data tokens.</param>
        /// <param name="routeHandler">The route handler.</param>
        public NamedRoute(string name, string displayName, string url, RouteValueDictionary defaults,
                          RouteValueDictionary constraints,
                          RouteValueDictionary dataTokens, IRouteHandler routeHandler)
            : base(url, defaults, constraints, dataTokens, routeHandler)
        {
            _name = name;
            _displayName = displayName;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NamedRoute"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="displayName">The display name.</param>
        /// <param name="url">The URL.</param>
        /// <param name="routeHandler">The route handler.</param>
        public NamedRoute(string name, string displayName, string url, MvcRouteHandler routeHandler)
            : base(url, routeHandler)
        {
            _name = name;
            _displayName = displayName;
        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        /// <summary>
        /// Gets or sets the display name.
        /// </summary>
        /// <value>The display name.</value>
        public string DisplayName
        {
            get { return _displayName ?? _name; }
            set { _displayName = value; }
        }

        /// <summary>
        /// Gets the children.
        /// </summary>
        /// <value>The children.</value>
        public List<NamedRoute> Children
        {
            get { return _childRoutes; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is child.
        /// </summary>
        /// <value><c>true</c> if this instance is child; otherwise, <c>false</c>.</value>
        public bool IsChild { get; set; }
    }
}