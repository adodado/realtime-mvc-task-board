#region

using System.Linq;
using RealtimeKanbanBoard.Models;
using ShortBus;

#endregion

namespace RealtimeKanbanBoard.QuerysCommands.BoardList
{
    public class GetBoardListsQueryHandler : IQueryHandler<GetBoardListsQuery, List[]>
    {
        /// <summary>
        ///     Handles the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>List[][].</returns>
        public List[] Handle(GetBoardListsQuery request)
        {
            using (var ctx = new BoardContext())
            {
                var board = ctx.Boards.FirstOrDefault(b => b.Id == request.Id);
                var lists = board.Lists.OrderBy(l => l.Order).ToArray();
                foreach (var list in lists)
                {
                    list.Tasks = list.Tasks.OrderBy(t => t.Order).ToList();
                }
                return lists;
            }
        }
    }
}