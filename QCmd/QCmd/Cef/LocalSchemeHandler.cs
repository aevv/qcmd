using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CefSharp;

namespace QCmd.Cef
{
    class LocalSchemeHandler : ISchemeHandler
    {
        public bool ProcessRequestAsync(IRequest request, ISchemeHandlerResponse response, OnRequestCompletedHandler requestCompletedCallback)
        {
            var uri = new Uri(request.Url);
            var fileName = string.Format("{0}{1}", uri.Authority, uri.AbsolutePath);

            if (fileName[fileName.Length - 1] == '/')
                fileName = fileName.Substring(0, fileName.Length - 1);

            if (File.Exists(fileName))
            {
                var fileBytes = File.ReadAllBytes(fileName);
                response.ResponseStream = new MemoryStream(fileBytes);

                switch (Path.GetExtension(fileName))
                {
                    case ".html":
                        response.MimeType = "text/html";
                        break;
                    case ".js":
                        response.MimeType = "text/javascript";
                        break;
                    case ".css":
                        response.MimeType = "text/css";
                        break;
                    default:
                        response.MimeType = "application/octet-stream";
                        break;
                }

                requestCompletedCallback();
                return true;
            }
            return false;
        }
    }

    class LocalSchemeHandlerFactory : ISchemeHandlerFactory
    {
        public ISchemeHandler Create()
        {
            return new LocalSchemeHandler();
        }
    }
}
