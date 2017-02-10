#region

using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;

#endregion

namespace RealtimeKanbanBoard.Models.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<BoardContext>
    {
        /// <summary>
        ///     The list identifier
        /// </summary>
        private int listId;

        /// <summary>
        ///     The list order
        /// </summary>
        private int listOrder;

        /// <summary>
        ///     Initializes a new instance of the <see cref="Configuration" /> class.
        /// </summary>
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        /// <summary>
        ///     Seeds the specified context.
        /// </summary>
        /// <param name="context">The context.</param>
        protected override void Seed(BoardContext context)
        {
            var taskId = 0;
            listOrder = 0;

            var board = new Board {Id = 1, Name = "Förslagsbacklog", Lists = new List<List>()};
            context.Boards.AddOrUpdate(board);
            board.Lists.Add(CreateList("Förslag"));
            board.Lists.Add(CreateList("Godkänd"));

            var lists = context.Set<List>();

            lists.AddOrUpdate(board.Lists.ToArray());
            context.SaveChanges();

            listOrder = 0;

            var board1 = new Board {Id = 2, Name = "ProduktBacklog", Lists = new List<List>()};
            context.Boards.AddOrUpdate(board1);
            context.SaveChanges();

            board1.Lists.Add(CreateList("Förslag"));
            board1.Lists.Add(CreateList("Design"));
            board1.Lists.Add(CreateList("Design Godkännd"));
            board1.Lists.Add(CreateList("Utveckling påbörjad"));
            board1.Lists.Add(CreateList("Utveckling klar"));
            board1.Lists.Add(CreateList("Testning"));
            board1.Lists.Add(CreateList("Levererad"));

            lists.AddOrUpdate(board1.Lists.ToArray());
            context.SaveChanges();
        }

        /// <summary>
        ///     Creates the list.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>List.</returns>
        private List CreateList(string name)
        {
            return new List {Id = listId++, Name = name, Tasks = null, Order = listOrder++};
        }
    }
}