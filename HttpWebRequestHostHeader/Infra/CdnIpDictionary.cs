using HttpWebRequestHostHeader.Infra.Repositories;
using HttpWebRequestHostHeader.Infra.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpWebRequestHostHeader.Infra
{
    public class CdnIpDictionary:Dictionary<string, bool>
    {
        private ICheckRepository repo;
        public CdnIpDictionary(ICheckRepository repo)
        {
            this.repo = repo;
            Load();
        }
        /// <summary>
        /// При создании данного словаря из конструктора вызывается этот метод. Метод получает веб адрес из БД - http://api.cdnvideo.ru:8888/0/edge?id=03124_kommersant
        /// Сервер по данному адресу отвечает строковым массивом Ip-адресов. Вероятно, это адреса серверов сети CDN. Каждый Ip-адрес добавляется в качестве ключа в данный словарь.
        /// Значение - true.
        /// </summary>
        public void Load()
        {
            this.Clear();
            var url_api_edge = repo.CallMethod(w => w.GetParams(4)).Result;
            var ur = new GetWebObject<ur>();
            string[] Ips = ur.GetObject(url_api_edge.Value).edge;
            foreach(string s in Ips)
            {
                Add(s, true);
            }
        }
        /// <summary>
        /// Данный метод перебирает все значения в данном словаре. Достав ссылку на первую KeyValuePair со значением Value - true, присваивает ей значение Value - false и возвращает его.
        /// Если KeyValuePair со значением Value - true закончились, то перезагружает словарь с помощью метода выше.
        /// </summary>
        /// <returns></returns>
        public string GetNext()
        {
            if(!Values.Any(w=> w))
            {
                   Load();
            }

            var ans = this.First(r => r.Value);
            this[ans.Key] = false;

            return ans.Key;
        }
    }
}
