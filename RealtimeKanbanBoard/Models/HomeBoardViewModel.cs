namespace RealtimeKanbanBoard.Models
{
    public class HomeBoardViewModel
    {
        /// <summary>
        ///     Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        public int Id { get; set; }

        /// <summary>
        ///     Gets or sets the board.
        /// </summary>
        /// <value>The board.</value>
        public Board Board { get; set; }
    }
}