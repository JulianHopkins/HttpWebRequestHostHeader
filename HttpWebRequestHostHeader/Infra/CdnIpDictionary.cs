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
        public void Load()
        {
            
            var url_api_edge = repo.CallMethod(w => w.GetParams(4)).Result;
            var ur = new GetWebObject<ur>();
            string[] Ips = ur.GetObject(url_api_edge.Value).edge;
            foreach(string s in Ips)
            {
                Add(s, true);
            }
        }

        public string GetNext()
        {
            if(!Values.Any(w=> w))
            {
                this.Clear();
                Load();
            }

            var ans = this.First(r => r.Value);
            this[ans.Key] = false;

            return ans.Key;
        }
    }
}
