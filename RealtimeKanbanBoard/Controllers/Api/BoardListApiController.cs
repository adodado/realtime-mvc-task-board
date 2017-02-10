#region

using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using RealtimeKanbanBoard.Models;

#endregion

namespace RealtimeKanbanBoard.Controllers
{
    public class BoardListApiController : ApiController
    {
        // GET api/boardapi
        /// <summary>
        ///     Gets this instance.
        /// </summary>
        /// <returns>IEnumerable{System.String}.</returns>
        public IEnumerable<string> Get()
        {
            return new[] {"this is from the boardlistapi controller", "value2"};
        }

        // GET api/boardapi/5
        /// <summary>
        ///     Gets the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>System.String.</returns>
        public string Get(int id)
        {
            return "value";
        }

        // POST api/boardapi
        /// <summary>
        ///     Posts the specified input.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns>List.</returns>
        public List Post([FromBody] CreateList input)
        {
            using (var ctx = new BoardContext())
            {
                var board = ctx.Boards.First(b => b.Id == input.BoardId);
                var list = new List {Name = input.name};
                board.Lists.Add(list);
                ctx.Set<List>().Add(list);
                ctx.SaveChanges();
                return list;
            }
        }

        // DELETE api/boardapi/5
        /// <summary>
        ///     Deletes the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        public void Delete([FromBody] CreateList value)
        {
            using (var ctx = new BoardContext())
            {
                var list = ctx.Lists.FirstOrDefault(a => a.BoardId == value.BoardId && a.Name == value.name);
                ctx.Lists.Remove(list);
                ctx.SaveChanges();
            }
        }

        // PUT api/boardapi/5
        /// <summary>
        ///     Puts the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        public void Put([FromBody] CreateList value)
        {
            using (var ctx = new BoardContext())
            {
                //TODO Add code for updating of list.

                //var list = ctx.Lists.FirstOrDefault(a => a.BoardId == value.BoardId && a.Name == value.name);
                //ctx.Lists.AddOrUpdate(list);
                //ctx.SaveChanges();
            }
        }
    }

    public class CreateList
    {
        /// <summary>
        ///     The board identifier
        /// </summary>
        public int BoardId;

        /// <summary>
        ///     The name
        /// </summary>
        public string name;
    }
}