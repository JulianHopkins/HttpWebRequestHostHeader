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


        public void CheckUrl()
        {
            checker.GetObject();
        }
        public void CheckIp(string Ip)
        {
            checker.GetObject(Ip);
        }
    }
}
