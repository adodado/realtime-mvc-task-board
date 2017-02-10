#region

using ShortBus;

#endregion

namespace RealtimeKanbanBoard.QuerysCommands.MoveList
{
    public class MoveListCommand : ICommand
    {
        /// <summary>
        ///     Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        public int Id { get; set; }

        /// <summary>
        ///     Gets or sets the list identifier.
        /// </summary>
        /// <value>The list identifier.</value>
        public int ListId { get; set; }

        /// <summary>
        ///     Gets or sets the index of the target.
        /// </summary>
        /// <value>The index of the target.</value>
        public int TargetIndex { get; set; }
    }
}