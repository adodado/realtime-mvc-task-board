#region

using System.Net.Http;
using System.Net.Http.Headers;

#endregion

namespace RealtimeKanbanBoard.Controllers
{
    public class CustomMultipartFormDataStreamProvider : MultipartFormDataStreamProvider
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="CustomMultipartFormDataStreamProvider" /> class.
        /// </summary>
        /// <param name="path">The path.</param>
        public CustomMultipartFormDataStreamProvider(string path)
            : base(path)
        {
        }

        /// <summary>
        ///     Gets the name of the local file which will be combined with the root path to create an absolute file name where the
        ///     contents of the current MIME body part will be stored.
        /// </summary>
        /// <param name="headers">The headers for the current MIME body part.</param>
        /// <returns>A relative filename with no path component.</returns>
        public override string GetLocalFileName(HttpContentHeaders headers)
        {
            var name = !string.IsNullOrWhiteSpace(headers.ContentDisposition.FileName)
                ? headers.ContentDisposition.FileName
                : "NoName";
            return name.Replace("\"", string.Empty);
        }
    }
}