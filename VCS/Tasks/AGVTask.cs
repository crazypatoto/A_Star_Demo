using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VCS.Maps;
using VCS.AGVs;
using VCS.Models;


namespace VCS.Tasks
{
    public abstract class AGVTask
    {
        public static readonly object _agvTaskLock = new object();
        public AGVTaskHandler Handler { get; }
        public AGV AssignedAGV { get; }
        public long StartTimeStamp { get; }
        public long FinishTimeStamp { get; private protected set; }
        public bool Finished { get; private protected set; }
        protected AGVTask(AGVTaskHandler handler)
        {
            this.Handler = handler;
            this.StartTimeStamp = DateTimeOffset.Now.ToUnixTimeSeconds();
            this.AssignedAGV = handler.AGV;
            this.Finished = false;
        }
        public abstract void Execute();
    }
}
