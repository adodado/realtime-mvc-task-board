#region

using System.Linq;
using RealtimeKanbanBoard.Models;
using ShortBus;

#endregion

namespace RealtimeKanbanBoard.QuerysCommands.HomeBoard
{
    public class HomeBoardViewModelQueryHandler : IQueryHandler<HomeBoardViewModelQuery, HomeBoardViewModel>
    {
        /// <summary>
        ///     Handles the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>HomeBoardViewModel.</returns>
        public HomeBoardViewModel Handle(HomeBoardViewModelQuery request)
        {
            var model = new HomeBoardViewModel();
            model.Id = request.Id;
            using (var ctx = new BoardContext())
            {
                model.Board = ctx.Boards.Include("Lists").First(board => board.Id == request.Id);
            }
            return model;
        }
    }
}