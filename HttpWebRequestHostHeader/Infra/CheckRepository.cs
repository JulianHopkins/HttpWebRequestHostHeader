using HttpWebRequestHostHeader.Infra.Repositories.Interfaces;
using HttpWebRequestHostHeader.Models;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpWebRequestHostHeader.Infra.Repositories
{
    public class CheckRepository : ICheckRepository
    {
        public async Task<R> CallMethod<R>(Func<ICheckRepository, Task<R>> func) where R : new()
        {

            try
            {
                return await func(this).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                Logger Logger = LogManager.GetCurrentClassLogger();
                Exception rex = null;
                StringBuilder errbody = new StringBuilder(3000);
                R answer = new R();
                var i = 0;

                while (ex != null)
                {
                    errbody.AppendLine(string.Format("\r\n{0} type: {1}, Message: {2}", ++i > 1 ? "Inner exception" : "Exception", ex.GetType(), ex.Message));
                    rex = ex;
                    ex = ex.InnerException;
                }


                Logger.Warn(rex, errbody.ToString());

                return answer;

            }
        }
        public Task<ParamsArr> GetParams(int[] Id)
        {
            using (CheckResponse chr = new CheckResponse())
            {
 
                    return Task.FromResult(new ParamsArr { Params = chr.Params.AsNoTracking().Where(y => Id.Contains(y.Id)).ToList() });

            }
        }
        public Task<Params> GetParams(int Id)
        {
            using (CheckResponse chr = new CheckResponse())
            {
                return Task.FromResult(chr.Params.AsNoTracking().FirstOrDefault(q=> q.Id == Id));
            }
        }

        public async Task<int> AddIpCheck(IpCheck check)
        {
            using (CheckResponse chr = new CheckResponse())
            {
                chr.IpCheck.Add(check);
                return await chr.SaveChangesAsync();
            }
        }






    }
}
