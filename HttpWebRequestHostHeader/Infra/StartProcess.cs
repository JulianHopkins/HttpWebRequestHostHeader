using HttpWebRequestHostHeader.Infra.Repositories;
using HttpWebRequestHostHeader.Infra.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpWebRequestHostHeader.Infra
{
    public class StartProcess
    {
        private string Check_Type;
        private readonly Lazy<CdnIpDictionary> CdnIps;
        private readonly ICheckRepository repo;
        private readonly CheckCdn checker;
        /// <summary>
        /// Передаём репозиторий в параметре конструктора. Загружаем из БД тип затребованной проверки. Создаём лениво загружаемый словарь CdnIpDictionary,
        /// который нужен только при проверке A, которая сейчас не используется, т.к. сервер CDN, загружающий данный словарь адресами не отвечает.
        /// </summary>
        /// <param name="repo"></param>
        public StartProcess(ICheckRepository repo)
        {
            this.repo = repo;
            //Загружаем из БД тип затребованной проверки.
            var parameter = repo.CallMethod(w => w.GetParams(2)).Result;
            Check_Type = parameter.Value;
            this.CdnIps = new Lazy<CdnIpDictionary>(()=> new CdnIpDictionary(repo));
            checker = new CheckCdn(repo);
        }
        /// <summary>
        /// Метод, постоянно вызываемый таймером, вызывает проверку затребованного типа и записывает результат проверки в БД.
        /// При проверке А, самозагружаемый с сервера CDN по uri словарь выдаёт ip-адреса проверяемых серверов по одному. Когда все адреса использованы, словарь перезагружается.
        /// </summary>
        public void Start()
        {
            if((Check_Type == "C") || (Check_Type == "B"))
            {

                checker.CheckUrl();
            }
            if ((Check_Type == "A") || (Check_Type == "B"))
            {
                checker.CheckIp(CdnIps.Value.GetNext());
            }
            Check_Type = repo.GetParams(2).Result.Value;
        }
        public void Stop()
        {

        }

    }
}
