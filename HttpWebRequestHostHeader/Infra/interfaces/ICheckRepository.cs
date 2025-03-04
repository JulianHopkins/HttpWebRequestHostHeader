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
        /// <summary>
        /// Стандартный метод репозитория, для обработки исключений в одном месте.
        /// </summary>
        /// <typeparam name="R"></typeparam>
        /// <param name="func"></param>
        /// <returns></returns>
        Task<R> CallMethod<R>(Func<ICheckRepository, Task<R>> func) where R : new();
        /// <summary>
        /// Возвращает List/Params/ из таблицы Params, Id которых входят в передаваемый в параметре массив.
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Task<Params> GetParams(int Id);
        /// <summary>
        /// Возвращает Params из таблицы Params, Id которого равен передаваемому в параметре int.
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Task<ParamsArr> GetParams(int[] Id);
        /// <summary>
        /// Добавляет экземпляр IpCheck в таблицу IpCheck.
        /// </summary>
        /// <param name="check"></param>
        /// <returns></returns>
        Task<int> AddIpCheck(IpCheck check);
    }
}
