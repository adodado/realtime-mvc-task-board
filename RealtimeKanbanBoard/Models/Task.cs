namespace RealtimeKanbanBoard.Models
{
    public class Task
    {
        /// <summary>
        ///     Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        public int Id { get; set; }

        /// <summary>
        ///     Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; set; }

        /// <summary>
        ///     Gets or sets the desc.
        /// </summary>
        /// <value>The desc.</value>
        public string Desc { get; set; }

        /// <summary>
        ///     Gets or sets the order.
        /// </summary>
        /// <value>The order.</value>
        public int Order { get; set; }
    }
}