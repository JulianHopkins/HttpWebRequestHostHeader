using HttpWebRequestHostHeader.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpWebRequestHostHeader.Infra.Repositories.Interfaces
{
    public interface ICheckRepository
    {
        Task<R> CallMethod<R>(Func<ICheckRepository, Task<R>> func) where R : new();

        Task<Params> GetParams(int Id);
        Task<ParamsArr> GetParams(int[] Id);

        Task<int> AddIpCheck(IpCheck check);
    }
}
