#region

using System.Linq;
using RealtimeKanbanBoard.Models;
using ShortBus;

#endregion

namespace RealtimeKanbanBoard.QuerysCommands.MoveTask
{
    public class MoveTaskCommandHandler : ICommandHandler<MoveTaskCommand>
    {
        /// <summary>
        ///     Handles the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void Handle(MoveTaskCommand message)
        {
            using (var ctx = new BoardContext())
            {
                var board = ctx.Boards.FirstOrDefault(b => b.Id == message.BoardId);
                if (message.SourceListId != message.DestinationListId)
                {
                    var source = board.Lists.First(l => l.Id == message.SourceListId);
                    var task = source.Tasks.First(t => t.Id == message.TaskId);
                    var destintation = board.Lists.First(l => l.Id == message.DestinationListId);
                    destintation.Tasks = destintation.Tasks.OrderBy(t => t.Order).ToList();
                    destintation.InsertTask(message.TargetIndex, task);
                    source.Tasks.Remove(task);
                    OrderTasks(source);
                    OrderTasks(destintation);
                }
                else
                {
                    var source = board.Lists.First(l => l.Id == message.SourceListId);
                    var task = source.Tasks.First(t => t.Id == message.TaskId);
                    source.Tasks.Remove(task);
                    source.InsertTask(message.TargetIndex, task);
                    OrderTasks(source);
                }
                ctx.SaveChanges();
            }
        }

        /// <summary>
        ///     Orders the tasks.
        /// </summary>
        /// <param name="source">The source.</param>
        private static void OrderTasks(List source)
        {
            var order = 0;
            foreach (var t in source.Tasks)
            {
                t.Order = order++;
            }
        }
    }
}