#region

using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;

#endregion

namespace RealtimeKanbanBoard.Controllers.Api
{
    public class MyConfigAttribute : Attribute, IControllerConfiguration
    {
        /// <summary>
        ///     Callback invoked to set per-controller overrides for this controllerDescriptor.
        /// </summary>
        /// <param name="controllerSettings">The controller settings to initialize.</param>
        /// <param name="controllerDescriptor">
        ///     The controller descriptor. Note that the
        ///     <see cref="T:System.Web.Http.Controllers.HttpControllerDescriptor" /> can be associated with the derived controller
        ///     type given that <see cref="T:System.Web.Http.Controllers.IControllerConfiguration" /> is inherited.
        /// </param>
        public void Initialize(HttpControllerSettings controllerSettings,
            HttpControllerDescriptor controllerDescriptor)
        {
            controllerSettings.Formatters.RemoveAt(1);
        }
    }

    [MyConfig]
    public class ValuesController : ApiController
    {
        // GET api/values
        /// <summary>
        ///     Gets this instance.
        /// </summary>
        /// <returns>IEnumerable{System.String}.</returns>
        public IEnumerable<string> Get()
        {
            return new[] {"value1", "value2"};
        }

        // GET api/values/5
        /// <summary>
        ///     Gets the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>System.String.</returns>
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        /// <summary>
        ///     Posts the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <exception cref="System.Web.Http.HttpResponseException"></exception>
        public void Post([FromBody] string value)
        {
            throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotAcceptable, "test"));
        }

        // PUT api/values/5
        /// <summary>
        ///     Puts the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="value">The value.</param>
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        /// <summary>
        ///     Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public void Delete(int id)
        {
        }
    }
}