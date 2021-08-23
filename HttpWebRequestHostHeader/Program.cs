using HttpWebRequestHostHeader.Infra;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HttpWebRequestHostHeader
{

    class Program
    {
        static void Main(string[] args)
        {

            var checkService = new CheckService();
            checkService.OnStart();
            Console.WriteLine("Служба запущена, нажмите любую клавишу для завершения...");
            Console.ReadKey();
            checkService.OnStop();
        }



    }

}
