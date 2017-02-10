#region

using System.Web.Mvc;
using BootstrapMvcSample.Controllers;
using RealtimeKanbanBoard.QuerysCommands.HomeBoard;
using RealtimeKanbanBoard.QuerysCommands.HomeIndex;
using ShortBus;
using WebMatrix.WebData;

#endregion

namespace RealtimeKanbanBoard.Controllers
{
    public class HomeController : BootstrapBaseController
    {
        /// <summary>
        ///     The _mediator
        /// </summary>
        private readonly IMediator _mediator;

        /// <summary>
        ///     Initializes a new instance of the <see cref="HomeController" /> class.
        /// </summary>
        /// <param name="mediator">The mediator.</param>
        public HomeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        ///     Indexes this instance.
        /// </summary>
        /// <returns>ActionResult.</returns>
        public ActionResult Index()
        {
            if (WebSecurity.IsAuthenticated)
            {
                var response = _mediator.Request(new HomeIndexQuery());
                return View(response.Data);
            }
            return RedirectToAction("Login", "User");
        }

        /// <summary>
        ///     Boards the specified identifier.
        /// </summary>
        /// <param name="Id">The identifier.</param>
        /// <returns>ActionResult.</returns>
        public ActionResult Board(int Id)
        {
            if (!WebSecurity.IsAuthenticated)
            {
                Response.Redirect("~/User/login");
            }

            var response = _mediator.Request(new HomeBoardViewModelQuery {Id = Id});
            if (response.HasException())
                Error(response.Exception.Message);
            return View(response.Data);
        }
    }
}