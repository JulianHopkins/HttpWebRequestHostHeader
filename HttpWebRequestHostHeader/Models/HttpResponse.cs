using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HttpWebRequestHostHeader.Models
{
    public class HttpResponse
    {
        public WebResponse Response { get; set; }
        public string ErrorMessage { get; set; }
    }
}
