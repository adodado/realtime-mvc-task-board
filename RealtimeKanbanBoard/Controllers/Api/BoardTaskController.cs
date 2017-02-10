#region

using System.Linq;
using System.Web.Http;
using RealtimeKanbanBoard.Models;

#endregion

namespace RealtimeKanbanBoard.Controllers.Api
{
    public class BoardTaskController : ApiController
    {
        // POST api/boardtask
        /// <summary>
        ///     Posts the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>Task.</returns>
        public Task Post([FromBody] CreateTask value)
        {
            using (var ctx = new BoardContext())
            {
                var board = ctx.Boards.First(b => b.Id == value.BoardId);
                var t = new Task {Name = value.name, Desc = value.desc};
                board.Lists.First().InsertTask(0, t);
                ctx.Set<Task>().Add(t);
                ctx.SaveChanges();
                return t;
            }
        }

        /// <summary>
        ///     Deletes the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        public void Delete([FromBody] CreateTask value)
        {
            //ToDo: Rewrite code for deleting the row
            var ctx = new BoardContext();
            ctx.Database.ExecuteSqlCommand("DELETE FROM Tasks WHERE Id=" + value.taskId);
        }

        /// <summary>
        ///     Puts the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        public void Put([FromBody] CreateTask value)
        {
            //ToDo: Rewrite code for updating the row
            var ctx = new BoardContext();
            ctx.Database.ExecuteSqlCommand("");
        }
    }

    public class CreateTask
    {
        /// <summary>
        ///     The board identifier
        /// </summary>
        public int BoardId;

        /// <summary>
        ///     The desc
        /// </summary>
        public string desc;

        /// <summary>
        ///     The name
        /// </summary>
        public string name;

        /// <summary>
        ///     The task identifier
        /// </summary>
        public int taskId;
    }
}