#region

using System.Linq;
using RealtimeKanbanBoard.Models;
using ShortBus;

#endregion

namespace RealtimeKanbanBoard.QuerysCommands.HomeIndex
{
    public class HomeIndexQueryHandler : IQueryHandler<HomeIndexQuery, HomeIndexModel>
    {
        /// <summary>
        ///     Handles the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>HomeIndexModel.</returns>
        public HomeIndexModel Handle(HomeIndexQuery request)
        {
            var model = new HomeIndexModel();
            using (var ctx = new BoardContext())
            {
                model.Boards = ctx.Boards.OrderBy(b => b.Name).ToList();
            }
            return model;
        }
    }
}