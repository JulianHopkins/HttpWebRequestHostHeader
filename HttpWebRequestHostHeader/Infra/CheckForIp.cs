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

        /// <summary>
        /// Конструктор данного класса не получает элементы тестируемого веб-адреса из БД, они ему передаются.
        /// </summary>
        /// <param name="repo"></param>
        /// <param name="response_timeout"></param>
        /// <param name="ping_path"></param>
        /// <param name="ping_scheme"></param>
        /// <param name="ping_domen"></param>
        public CheckForIp(ICheckRepository repo, int response_timeout, string ping_path, string ping_scheme, string ping_domen)
        {
            this.response_timeout = response_timeout;
            this.ping_path = ping_path;
            this.ping_scheme = ping_scheme;
            this.ping_domen = ping_domen;
            this.repo = repo;
            
        }
        /// <summary>
        /// Проверка типа A. При этой проверке значение IP-адреса известно заранее. Чтобы серверу понять по какому домену сделан запрос на данный IP-адрес, указываем 
        /// обязательный заголовок Host в нашем клиентском GET-запросе.
        /// </summary>
        /// <param name="IP"></param>
        public void GetObject(string IP)
        {
            var check = new IpCheck();
            check.IpAddr = IP;
            check.CheckType = "A";
            var request = (HttpWebRequest)WebRequest.Create($"{ping_scheme}://{IP}{ping_path}");
            request.SetRawHeader("Host", ping_domen);
            ExecuteRequest(request, check);
        }
        /// <summary>
        /// Проверка типа С. В которой нас интересует с какого ip-адреса нам ответит сервер сети CDN, если мы отправляем запрос на общий домен: cache-kommersant.cdnvideo.ru
        /// </summary>
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
        /// <summary>
        /// Получает объекты check и request, request обогащается захардкоженными данными и исполняется.
        /// далее check обогащается результатами запроса и сохраняется в БД.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="check"></param>
        private async void ExecuteRequest(HttpWebRequest request, IpCheck check)
        {
            //Устанавливаем параметры запроса (кроме url)
            request.Method = "GET";
            request.ContentType = "text/html; charset=utf-8";
            request.Accept = "*/*";
            request.UserAgent = "Mozilla/4.0 (compatible; MSIE 5.01; Windows NT 5.0)";
            request.Timeout = response_timeout;
            request.KeepAlive = false;
            //Объект, представляющий сетевое подключение к интернет ресурсу.
            request.ServicePoint.ConnectionLeaseTimeout = 0;
            //Проверка сертификата сервера всегда успешна.
            ServicePointManager.ServerCertificateValidationCallback = (s, certificate, chain, sslPolicyErrors) => true;
            //Устанавливаем время нашего запроса в объекте DTO - check.
            check.ReqTime = DateTime.Now;
            Stopwatch sw = Stopwatch.StartNew();
            var answer = await base.answer(request);
            //Устанавливаем время обработки запроса в объекте DTO - check.
            check.ResSpan = sw.ElapsedMilliseconds;
            if (!String.IsNullOrWhiteSpace(answer.ErrorMessage))
            {
                Logger.Warn(answer.ErrorMessage);
                //Устанавливаем ErrorMessage запроса в объекте DTO - check.
                check.ErrorMessage = answer.ErrorMessage;
            }


            if(answer.Response != null)
            {
                //Чтобы получить код статуса нам нужно кастить Responce (WebResponse) к HttpWebResponse
                check.ResCode = ((int)((HttpWebResponse)answer.Response).StatusCode).ToString();
                check.ContentLength = answer.Response.ContentLength;

                answer.Response.Close();
                answer.Response.Dispose();
            }
            if (check.CheckType == "C")
            {
                //Если заданный тип проверки - C, получаем Ip-адрес сервера.
                check.IpAddr = remoteEP?.ToString()?.Split(':')[0];
            }
            //Добавляем check в таблицу БД.
            await repo.CallMethod<int>(async w=> await w.AddIpCheck(check));
        }

    }

}
