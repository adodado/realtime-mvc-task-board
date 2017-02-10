#region

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#endregion

namespace RealtimeKanbanBoard.Models
{
    public class Board
    {
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
        [Required]
        public string Name { get; set; }

        /// <summary>
        ///     Gets or sets the desc.
        /// </summary>
        /// <value>The desc.</value>
        public string Desc { get; set; }

        /// <summary>
        ///     Gets or sets the lists.
        /// </summary>
        /// <value>The lists.</value>
        public virtual ICollection<List> Lists { get; set; }
    }
}