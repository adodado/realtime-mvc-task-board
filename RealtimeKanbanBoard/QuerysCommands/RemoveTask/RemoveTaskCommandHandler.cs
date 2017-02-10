#region

using System.Linq;
using RealtimeKanbanBoard.Models;
using ShortBus;

#endregion

namespace RealtimeKanbanBoard.QuerysCommands.RemoveTask
{
    public class RemoveTaskCommandHandler : ICommandHandler<RemoveTaskCommand>
    {
        /// <summary>
        ///     Handles the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void Handle(RemoveTaskCommand message)
        {
            using (var ctx = new BoardContext())
            {
                var board = ctx.Boards.FirstOrDefault(b => b.Id == message.BoardId);
                var source = board.Lists.First(l => l.Id == message.SourceListId);
                var task = source.Tasks.First(t => t.Id == message.TaskId);
                source.Tasks.Remove(task);
                OrderTasks(source);
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