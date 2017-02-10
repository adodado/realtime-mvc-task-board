#region

using System.Web.Mvc;
using WebMatrix.WebData;

#endregion

namespace RealtimeKanbanBoard.Controllers
{
    public class UserController : Controller
    {
        /// <summary>
        ///     Registers this instance.
        /// </summary>
        /// <returns>ActionResult.</returns>
        [HttpGet]
        public ActionResult Register()
        {
            if (!WebSecurity.Initialized)
            {
                WebSecurity.InitializeDatabaseConnection("BoardContext", "UserProfile", "UserId", "UserName", true);
            }
            return View();
        }

        /// <summary>
        ///     Registers the specified form.
        /// </summary>
        /// <param name="form">The form.</param>
        /// <returns>ActionResult.</returns>
        [HttpPost]
        public ActionResult Register(FormCollection form)
        {
            WebSecurity.CreateUserAndAccount(form["username"], form["password"]);
            return RedirectToAction("Login", "User");
        }

        /// <summary>
        ///     Logins this instance.
        /// </summary>
        /// <returns>ActionResult.</returns>
        [HttpGet]
        public ActionResult Login()
        {
            if (!WebSecurity.Initialized)
            {
                WebSecurity.InitializeDatabaseConnection("BoardContext", "UserProfile", "UserId", "UserName", true);
            }
            return View();
        }

        /// <summary>
        ///     Logins the specified form.
        /// </summary>
        /// <param name="form">The form.</param>
        /// <returns>ActionResult.</returns>
        [HttpPost]
        public ActionResult Login(FormCollection form)
        {
            var success = WebSecurity.Login(form["username"], form["password"], false);
            if (success)
            {
                var returnUrl = Request.QueryString["ReturnUrl"];
                if (returnUrl == null)
                {
                    return RedirectToAction("Index", "Home");
                }
                Response.Redirect(returnUrl);
            }
            return View();
        }

        /// <summary>
        ///     Logouts this instance.
        /// </summary>
        /// <returns>ActionResult.</returns>
        public ActionResult Logout()
        {
            WebSecurity.Logout();
            return RedirectToAction("Login", "User");
        }
    }
}