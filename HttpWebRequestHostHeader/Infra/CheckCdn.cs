using HttpWebRequestHostHeader.Infra.Repositories;
using HttpWebRequestHostHeader.Infra.Repositories.Interfaces;
using HttpWebRequestHostHeader.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpWebRequestHostHeader.Infra
{
    /// <summary>
    /// Данный клас это надстройка над CheckForIp checker, который инстанцируется в конструкторе. Именно здесь мы загружаем из БД все элементы тестируемого uri
    /// и передаём в CheckForIp.
    /// </summary>
    public class CheckCdn
    {
        private readonly ICheckRepository repo;
        private readonly CheckForIp checker;
        public CheckCdn(ICheckRepository repo)
        {
            this.repo = repo;
            var parameters = repo.CallMethod(w=> w.GetParams(new int[] { 1, 3, 5, 6 })).Result;
            int response_timeout = int.Parse(parameters.Params.First(t => t.Name == "response_timeout").Value);
            string ping_path = parameters.Params.First(t => t.Name == "ping_path").Value;
            string ping_scheme = parameters.Params.First(t => t.Name == "ping_scheme").Value;
            string ping_domen = parameters.Params.First(t => t.Name == "ping_domen").Value;
            checker = new CheckForIp(repo, response_timeout, ping_path, ping_scheme, ping_domen);
        }
        /// <summary>
        /// Проверка типа С. В которой нас интересует с какого ip-адреса нам ответит сервер сети CDN, если мы отправляем запрос на общий домен: cache-kommersant.cdnvideo.ru
        /// </summary>
        public void CheckUrl()
        {
            checker.GetObject();
        }
        /// <summary>
        /// Проверка типа A. При этой проверке значение IP-адреса известно заранее. Чтобы серверу понять по какому домену сделан запрос на данный IP-адрес, указываем 
        /// обязательный заголовок Host в нашем клиентском GET-запросе.
        /// </summary>
        public void CheckIp(string Ip)
        {
            checker.GetObject(Ip);
        }
    }
}
