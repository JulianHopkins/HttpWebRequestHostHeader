using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpWebRequestHostHeader.Infra
{
    /// <summary>
    /// DTO с единственным свойством string[] edge, в который десериализуется ответ json от сервера.
    /// </summary>
    public class ur
    {
        public string[] edge { get; set; }
    }
}
