﻿#region

using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using BootstrapMvcSample;
using BootstrapSupport;

#endregion

namespace RealtimeKanbanBoard
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteTable.Routes.MapHubs();
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BootstrapBundleConfig.RegisterBundles(BundleTable.Bundles);
            ExampleLayoutsRouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}