#region

using ShortBus;

#endregion

namespace RealtimeKanbanBoard.QuerysCommands.RemoveTask
{
    public class RemoveTaskCommand : ICommand
    {
        /// <summary>
        ///     Gets or sets the board identifier.
        /// </summary>
        /// <value>The board identifier.</value>
        public int BoardId { get; set; }

        /// <summary>
        ///     Gets or sets the source list identifier.
        /// </summary>
        /// <value>The source list identifier.</value>
        public int SourceListId { get; set; }

        /// <summary>
        ///     Gets or sets the task identifier.
        /// </summary>
        /// <value>The task identifier.</value>
        public int TaskId { get; set; }

        /// <summary>
        ///     Gets or sets the index of the target.
        /// </summary>
        /// <value>The index of the target.</value>
        public int TargetIndex { get; set; }
    }
}