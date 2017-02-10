#region

using System.Data.Entity;

#endregion

namespace RealtimeKanbanBoard.Models
{
    public class BoardContext : DbContext
    {
        /// <summary>
        ///     Gets or sets the boards.
        /// </summary>
        /// <value>The boards.</value>
        public DbSet<Board> Boards { get; set; }

        /// <summary>
        ///     Gets or sets the lists.
        /// </summary>
        /// <value>The lists.</value>
        public DbSet<List> Lists { get; set; }

        /// <summary>
        ///     Gets or sets the tasks.
        /// </summary>
        /// <value>The tasks.</value>
        public DbSet<Task> Tasks { get; set; }
    }
}