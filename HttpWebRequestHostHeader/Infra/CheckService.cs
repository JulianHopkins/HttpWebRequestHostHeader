using HttpWebRequestHostHeader.Infra.Repositories;
using HttpWebRequestHostHeader.Infra.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace HttpWebRequestHostHeader.Infra
{
    public class CheckService
    {
        private StartProcess process;
        private long tick;
        private ICheckRepository repo;
        public CheckService()
        {
            this.repo = new CheckRepository();
            tick = long.Parse(repo.CallMethod(w => w.GetParams(7)).Result.Value);
            process = new StartProcess(repo);


        }
        public void OnStart()
        {
            Timer timer = new Timer();
            timer.Interval = tick; // 60 seconds
            timer.Elapsed += new ElapsedEventHandler(this.OnTimer);
            timer.Start();
        }
        public void OnTimer(object sender, ElapsedEventArgs args)
        {
            // TODO: Insert monitoring activities here.
            process.Start();
        }
        public void OnStop()
        {
            process.Stop();
            tick = long.Parse(repo.CallMethod(w => w.GetParams(7)).Result.Value);
        }
    }
}
