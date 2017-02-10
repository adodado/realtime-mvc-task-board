#region

using System.Web.Http.Dependencies;
using StructureMap;

#endregion

namespace RealtimeKanbanBoard.DependencyResolution
{
    /// <summary>
    ///     The structure map dependency resolver.
    /// </summary>
    public class StructureMapDependencyResolver : StructureMapDependencyScope, IDependencyResolver
    {
        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="StructureMapDependencyScope" /> class.
        /// </summary>
        /// <param name="container">The container.</param>
        public StructureMapDependencyResolver(IContainer container)
            : base(container)
        {
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     Starts a resolution scope.
        /// </summary>
        /// <returns>The dependency scope.</returns>
        public IDependencyScope BeginScope()
        {
            var child = Container.GetNestedContainer();
            return new StructureMapDependencyResolver(child);
        }

        #endregion
    }
}