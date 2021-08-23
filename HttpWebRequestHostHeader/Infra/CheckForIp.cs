using HttpWebRequestHostHeader.Infra.Repositories;
using HttpWebRequestHostHeader.Infra.Repositories.Interfaces;
using NLog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HttpWebRequestHostHeader.Infra
{
    public class CheckForIp : Sender
    {
        private readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private readonly int response_timeout;
        private readonly string ping_path;
        private readonly string ping_scheme;
        private readonly string ping_domen;
        private readonly ICheckRepository repo;
        private IPEndPoint remoteEP;


        public CheckForIp(ICheckRepository repo, int response_timeout, string ping_path, string ping_scheme, string ping_domen)
        {
            this.response_timeout = response_timeout;
            this.ping_path = ping_path;
            this.ping_scheme = ping_scheme;
            this.ping_domen = ping_domen;
            this.repo = repo;
            
        }
        public void GetObject(string IP)
        {
            var check = new IpCheck();
            check.IpAddr = IP;
            check.CheckType = "A";
            var request = (HttpWebRequest)WebRequest.Create($"{ping_scheme}://{IP}{ping_path}");
            request.SetRawHeader("Host", ping_domen);
            ExecuteRequest(request, check);
        }
        public void GetObject()
        {
            
            var check = new IpCheck();
            var request = (HttpWebRequest)WebRequest.Create($"{ping_scheme}://{ping_domen}{ping_path}");
            request.ServicePoint.BindIPEndPointDelegate = delegate (ServicePoint servicePoint, IPEndPoint remoteEndPoint, int retryCount) {
                remoteEP = remoteEndPoint;
                return null;
            };
            check.CheckType = "C";
            ExecuteRequest(request, check);

        }
        private async void ExecuteRequest(HttpWebRequest request, IpCheck check)
        {
            request.Method = "GET";
            request.ContentType = "text/html; charset=utf-8";
            request.Accept = "*/*";
            request.UserAgent = "Mozilla/4.0 (compatible; MSIE 5.01; Windows NT 5.0)";
            request.Timeout = response_timeout;
            ServicePointManager.ServerCertificateValidationCallback = (s, certificate, chain, sslPolicyErrors) => true;
            check.ReqTime = DateTime.Now;
            Stopwatch sw = Stopwatch.StartNew();
            var answer = await base.answer(request);
            check.ResSpan = sw.ElapsedMilliseconds;
            if (!String.IsNullOrWhiteSpace(answer.ErrorMessage))
            {
                Logger.Warn(answer.ErrorMessage);
                check.ErrorMessage = answer.ErrorMessage;
            }


            if(answer.Response != null)
            { 
            check.ResCode = ((int)((HttpWebResponse)answer.Response).StatusCode).ToString();
            check.ContentLength = answer.Response.ContentLength;

                answer.Response.Close();
                answer.Response.Dispose();
            }
            if (check.CheckType == "C")
            {
                check.IpAddr = remoteEP?.ToString()?.Split(':')[0];
            }

            await repo.CallMethod<int>(async w=> await w.AddIpCheck(check));
        }

    }

}
