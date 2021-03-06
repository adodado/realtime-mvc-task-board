﻿#region

using RealtimeKanbanBoard.Models;
using ShortBus;

#endregion

namespace RealtimeKanbanBoard.QuerysCommands.BoardList
{
    public class GetBoardListsQuery : IQuery<List[]>
    {
        /// <summary>
        ///     Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        public int Id { get; set; }
    }
}