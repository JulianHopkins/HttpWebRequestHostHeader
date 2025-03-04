using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HttpWebRequestHostHeader.Models
{
    /// <summary>
    /// Надстройка над WebResponse, включающая также свойство ErrorMessage.
    /// </summary>
    public class HttpResponse
    {
        /// <summary>
        /// Надстройка над WebResponse, включающая также свойство ErrorMessage.
        /// </summary>
        public HttpResponse () {}
        public WebResponse Response { get; set; }
        public string ErrorMessage { get; set; }
    }
}
