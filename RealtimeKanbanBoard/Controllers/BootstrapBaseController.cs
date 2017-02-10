using System.Web.Mvc;
using BootstrapSupport;

namespace BootstrapMvcSample.Controllers
{
    public class BootstrapBaseController : Controller
    {
        /// <summary>
        /// Attentions the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void Attention(string message)
        {
            TempData.Add(Alerts.ATTENTION, message);
        }

        /// <summary>
        /// Successes the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void Success(string message)
        {
            TempData.Add(Alerts.SUCCESS, message);
        }

        /// <summary>
        /// Informations the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void Information(string message)
        {
            TempData.Add(Alerts.INFORMATION, message);
        }

        /// <summary>
        /// Errors the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void Error(string message)
        {
            TempData.Add(Alerts.ERROR, message);
        }
    }
}