#region

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

#endregion

namespace RealtimeKanbanBoard.Models
{
    public class List
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="List" /> class.
        /// </summary>
        public List()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="List" /> class.
        /// </summary>
        /// <param name="tasks">The tasks.</param>
        public List(IList<Task> tasks)
        {
            Tasks = tasks.ToArray();
        }

        /// <summary>
        ///     Gets or sets the tasks.
        /// </summary>
        /// <value>The tasks.</value>
        public virtual ICollection<Task> Tasks { get; set; }

        /// <summary>
        ///     Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        [Key]
        public int Id { get; set; }

        /// <summary>
        ///     Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; set; }

        /// <summary>
        ///     Gets or sets the order.
        /// </summary>
        /// <value>The order.</value>
        public int Order { get; set; }

        /// <summary>
        ///     Gets or sets the board identifier.
        /// </summary>
        /// <value>The board identifier.</value>
        public int BoardId { get; set; }
    }
}