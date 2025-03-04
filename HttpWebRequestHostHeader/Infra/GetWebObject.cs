using HttpWebRequestHostHeader.Models;
using Newtonsoft.Json;
using NLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HttpWebRequestHostHeader.Infra
{
    /// <summary>
    /// Класс содержит единственный метод GetObject, который формирует web-запрос клиента (GET), ожидается десерилизация ответа от сервера в возвращаемый тип T.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class GetWebObject<T> : Sender where T:new()
    {
        private readonly Logger Logger = LogManager.GetCurrentClassLogger();
        /// <summary>
        /// Метод формирует web-запрос клиента (GET), ожидается десерилизация ответа от сервера в возвращаемый тип T.
        /// </summary>
        /// <param name="url">Адрес сервера.</param>
        /// <returns></returns>
        public T GetObject(string url)
        {
            
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.ContentType = "application/json; charset=utf-8";
            request.Accept = "application/json, text/javascript,*/*";
            request.UserAgent = "Mozilla/4.0 (compatible; MSIE 5.01; Windows NT 5.0)";
            HttpResponse response;
            Task<HttpResponse> Tresponse = base.answer(request);
            Tresponse.Wait();
            response = Tresponse.Result;
            if (!String.IsNullOrWhiteSpace(response.ErrorMessage)) Logger.Warn(response.ErrorMessage);
            string result;
            using (var streamReader = new StreamReader(response.Response.GetResponseStream()))
            {
                result = streamReader.ReadToEnd();
            }
            response.Response.Close();
            response.Response.Dispose();
            return JsonConvert.DeserializeObject<T>(result);
        }
    }
}
