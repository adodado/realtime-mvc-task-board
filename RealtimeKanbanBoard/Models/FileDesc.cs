#region

using System.Runtime.Serialization;

#endregion

namespace RealtimeKanbanBoard.Models
{
    [DataContract]
    public class FileDesc
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="FileDesc" /> class.
        /// </summary>
        /// <param name="n">The n.</param>
        /// <param name="p">The p.</param>
        /// <param name="s">The s.</param>
        public FileDesc(string n, string p, long s)
        {
            Name = n;
            Path = p;
            Size = s;
        }

        /// <summary>
        ///     Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        [DataMember]
        public string Name { get; set; }

        /// <summary>
        ///     Gets or sets the path.
        /// </summary>
        /// <value>The path.</value>
        [DataMember]
        public string Path { get; set; }

        /// <summary>
        ///     Gets or sets the size.
        /// </summary>
        /// <value>The size.</value>
        [DataMember]
        public long Size { get; set; }

        /// <summary>
        ///     Gets or sets the board identifier.
        /// </summary>
        /// <value>The board identifier.</value>
        [DataMember]
        public int BoardID { get; set; }
    }
}