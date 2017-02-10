#region

using System.Linq;
using System.Web.Mvc;
using RealtimeKanbanBoard.Models;
using ShortBus;

#endregion

namespace RealtimeKanbanBoard.Controllers
{
    public class BoardController : Controller
    {
        /// <summary>
        ///     The _context
        /// </summary>
        private readonly BoardContext _context;

        /// <summary>
        ///     The _mediator
        /// </summary>
        private readonly IMediator _mediator;

        /// <summary>
        ///     Initializes a new instance of the <see cref="BoardController" /> class.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="mediator">The mediator.</param>
        public BoardController(BoardContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }


        /// <summary>
        ///     Indexes this instance.
        /// </summary>
        /// <returns>ActionResult.</returns>
        public ActionResult Index()
        {
            var response = _mediator.Request(new BoardListQuery());
            return View(response.Data);
        }


        /// <summary>
        ///     Detailses the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>ActionResult.</returns>
        public ActionResult Details(int id)
        {
            var response = _mediator.Request(new BoardQuery {Id = id});
            return View(response.Data);
        }

        /// <summary>
        ///     Creates this instance.
        /// </summary>
        /// <returns>ActionResult.</returns>
        public ActionResult Create()
        {
            return View(new Board());
        }


        /// <summary>
        ///     Creates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>ActionResult.</returns>
        [HttpPost]
        public ActionResult Create(Board model)
        {
            if (ModelState.IsValid)
            {
                _context.Boards.Add(model);
                _context.SaveChanges();
                return RedirectToAction("index");
            }
            return View(model);
        }


        /// <summary>
        ///     Edits the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>ActionResult.</returns>
        public ActionResult Edit(int id)
        {
            var model = _context.Boards.First(board => board.Id == id);
            return View("create", model);
        }

        /// <summary>
        ///     Edits the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>ActionResult.</returns>
        [HttpPost]
        public ActionResult Edit(Board model)
        {
            if (ModelState.IsValid)
            {
                _context.Boards.Attach(model);
                return RedirectToAction("index");
            }

            return View("create", model);
        }
    }

    public class BoardQuery : IQuery<Board>
    {
        /// <summary>
        ///     Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        public int Id { get; set; }
    }

    public class BoardQueryHandler : IQueryHandler<BoardQuery, Board>
    {
        /// <summary>
        ///     Handles the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>Board.</returns>
        public Board Handle(BoardQuery request)
        {
            using (var _context = new BoardContext())
            {
                return _context.Boards.AsNoTracking().Include("Lists.Name").First(board => board.Id == request.Id);
            }
        }
    }

    public class BoardListQuery : IQuery<BoardList[]>
    {
    }

    public class BoardListQueryHandler : IQueryHandler<BoardListQuery, BoardList[]>
    {
        /// <summary>
        ///     Handles the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>BoardList[][].</returns>
        public BoardList[] Handle(BoardListQuery request)
        {
            using (var _context = new BoardContext())
            {
                return _context.Boards.Select(b => new BoardList {Name = b.Name, Id = b.Id}).ToArray();
            }
        }
    }

    public class BoardList
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
    }
}