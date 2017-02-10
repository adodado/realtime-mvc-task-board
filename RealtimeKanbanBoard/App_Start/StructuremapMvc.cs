#region

using System.Web.Http;
using System.Web.Mvc;
using RealtimeKanbanBoard;
using RealtimeKanbanBoard.DependencyResolution;
using WebActivator;

#endregion

[assembly: PreApplicationStartMethod(typeof (StructuremapMvc), "Start")]

namespace RealtimeKanbanBoard
{
    public static class StructuremapMvc
    {
        /// <summary>
        ///     Starts this instance.
        /// </summary>
        public static void Start()
        {
            var container = IoC.Initialize();
            DependencyResolver.SetResolver(new StructureMapDependencyResolver(container));
            GlobalConfiguration.Configuration.DependencyResolver = new StructureMapDependencyResolver(container);
        }
    }
}