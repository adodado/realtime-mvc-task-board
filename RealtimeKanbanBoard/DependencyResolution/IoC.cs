#region

using ShortBus;
using StructureMap;

#endregion

namespace RealtimeKanbanBoard.DependencyResolution
{
    public static class IoC
    {
        /// <summary>
        ///     Initializes this instance.
        /// </summary>
        /// <returns>IContainer.</returns>
        public static IContainer Initialize()
        {
            ObjectFactory.Initialize(i => i.Scan(s =>
            {
                s.AssemblyContainingType<IMediator>();
                s.TheCallingAssembly();
                s.WithDefaultConventions();
                s.ConnectImplementationsToTypesClosing((typeof (IQueryHandler<,>)));
                s.AddAllTypesOf(typeof (ICommandHandler<>));
            }));
            return ObjectFactory.Container;
        }
    }
}