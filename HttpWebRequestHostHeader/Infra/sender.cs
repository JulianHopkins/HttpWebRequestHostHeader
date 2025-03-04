using HttpWebRequestHostHeader.Models;
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
    /// Отправитель запросов серверу
    /// </summary>
    public class Sender
    {

        public Sender()
        {

        }
        /// <summary>
        /// Получает объект HttpWebRequest и отправляет запрос, при ответе создаёт WebResponse и вставляет его в поле Response объекта HttpResponse namespace HttpWebRequestHostHeader.
        /// Если сервер не ответил, то поле Response будет равно null, а в ErrorMessage - сообщение об истечении таймаута.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<HttpResponse> answer(HttpWebRequest request)
        {

            string result = string.Empty;
            WebResponse response = null;
            try
            {

                
                response = await request.GetResponseAsync();



            }
            catch (WebException we)
            {
                result = we.Message;
                if (we.Response != null)
                {
                    response = we.Response;
                }

            }
            catch (Exception ex)
            {
                while (ex != null)
                {
                    result += ex.Message;
                    result += Environment.NewLine;
                    ex = ex.InnerException;
                }

            }
            return new HttpResponse { Response = response, ErrorMessage = result };
        }

    }
}
