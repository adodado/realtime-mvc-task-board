#region

using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web.Http;
using RealtimeKanbanBoard.Models;

#endregion

namespace RealtimeKanbanBoard.Controllers.Api
{
    public class BoardBoardApiController : ApiController
    {
        // GET api/boardboardapi
        /// <summary>
        ///     Gets this instance.
        /// </summary>
        /// <returns>IEnumerable{System.String}.</returns>
        public IEnumerable<string> Get()
        {
            return new[] {"this is from the boardBoardapi controller", "value2"};
        }

        // GET api/boardboardapi
        /// <summary>
        ///     Gets the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>System.String.</returns>
        public string Get(int id)
        {
            //TODO!!!!
            return "value";
        }

        // POST api/boardboardapi
        /// <summary>
        ///     Posts the specified input.
        /// </summary>
        /// <param name="input">The input.</param>
        public void Post([FromBody] CreateBoard input)
        {
            using (var ctx = new BoardContext())
            {
                var board = ctx.Boards.Create();
                board.Name = input.name;
                board.Desc = input.desc;
                ctx.Boards.Add(board);
                ctx.SaveChanges();
            }
        }

        // DELETE api/boardboardapi
        /// <summary>
        ///     Deletes the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        public void Delete([FromBody] CreateBoard value)
        {
            using (var ctx = new BoardContext())
            {
                var board = ctx.Boards.FirstOrDefault(a => a.Id == value.id);
                var lists = ctx.Lists.Where(a => a.BoardId == value.id).ToList();

                foreach (var list in lists)
                {
                    ctx.Lists.Remove(list);
                }

                ctx.Boards.Remove(board);
                ctx.SaveChanges();
            }
        }

        /// <summary>
        ///     Updates the specified board.
        /// </summary>
        /// <param name="value">The board.</param>
        public void Update([FromBody] CreateBoard value)
        {
            using (var ctx = new BoardContext())
            {
                var board = ctx.Boards.FirstOrDefault(a => a.Id == value.id);
                if (board == null) return;

                board.Name = value.name;
                board.Desc = value.desc;
                ctx.Boards.AddOrUpdate(board);
                ctx.SaveChanges();
            }
        }
    }

    public class CreateBoard
    {
        /// <summary>
        ///     The desc
        /// </summary>
        public string desc;

        /// <summary>
        ///     The identifier
        /// </summary>
        public int id;

        /// <summary>
        ///     The name
        /// </summary>
        public string name;
    }
}