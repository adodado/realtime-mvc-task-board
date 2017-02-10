#region

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using RealtimeKanbanBoard.Models;

#endregion

namespace RealtimeKanbanBoard.Controllers.Api
{
    public class UploadController : ApiController
    {
        /// <summary>
        ///     Posts this instance.
        /// </summary>
        /// <returns>Task{IEnumerable{FileDesc}}.</returns>
        /// <exception cref="System.Web.Http.HttpResponseException"></exception>
        public Task<IEnumerable<FileDesc>> Post()
        {
            var folderName = "uploads";
            var PATH = HttpContext.Current.Server.MapPath("~/" + folderName);
            var rootUrl = Request.RequestUri.AbsoluteUri.Replace(Request.RequestUri.AbsolutePath, String.Empty);

            if (Request.Content.IsMimeMultipartContent())
            {
                var streamProvider = new CustomMultipartFormDataStreamProvider(PATH);
                var task =
                    Request.Content.ReadAsMultipartAsync(streamProvider).ContinueWith(t =>
                    {
                        if (t.IsFaulted || t.IsCanceled)
                        {
                            throw new HttpResponseException(HttpStatusCode.InternalServerError);
                        }

                        var fileInfo = streamProvider.FileData.Select(i =>
                        {
                            var info = new FileInfo(i.LocalFileName);
                            return new FileDesc(info.Name, rootUrl + "/" + folderName + "/" + info.Name,
                                info.Length/1024);
                        });
                        return fileInfo;
                    });

                return task;
            }
            throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotAcceptable,
                "This request is not properly formatted"));
        }
    }
}