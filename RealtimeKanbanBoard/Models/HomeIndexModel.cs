#region

using System.Collections.Generic;

#endregion

namespace RealtimeKanbanBoard.Models
{
    public class HomeIndexModel
    {
        /// <summary>
        ///     Gets or sets the boards.
        /// </summary>
        /// <value>The boards.</value>
        public List<Board> Boards { get; set; }
    }
}