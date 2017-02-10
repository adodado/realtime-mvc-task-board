#region

using System.Linq;
using RealtimeKanbanBoard.Models;
using ShortBus;

#endregion

namespace RealtimeKanbanBoard.QuerysCommands.MoveList
{
    public class MoveListCommandHandler : ICommandHandler<MoveListCommand>
    {
        /// <summary>
        ///     Handles the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        public void Handle(MoveListCommand request)
        {
            using (var ctx = new BoardContext())
            {
                var board = ctx.Boards.FirstOrDefault(b => b.Id == request.Id);
                var list = board.Lists.First(l => l.Id == request.ListId);

                var lists = board.Lists.ToList();
                lists.Remove(list);
                lists.Insert(request.TargetIndex, list);
                board.Lists = lists;
                var order = 0;
                foreach (var list1 in board.Lists)
                {
                    list1.Order = order++;
                }
                ctx.SaveChanges();
            }
        }
    }
}