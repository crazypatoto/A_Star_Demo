using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;
using VCS.Maps;
using VCS.Tasks;

namespace VCS.AGVs
{
    public class AGVHandler
    {
        private readonly CancellationTokenSource _cts;
        public VCS VCS { get; }
        public List<AGV> AGVList { get; private set; }
        public AGVHandler(VCS vcs)
        {
            this.VCS = vcs;
            AGVList = new List<AGV>();
            _cts = new CancellationTokenSource();
            Task.Run(AGVHandling);
        }

        public AGV AddSimulatedAGV(MapNode node, string name = null)
        {
            if (node == null) return null;
            int newID = 0;
            if (AGVList.Count != 0)
            {
                newID = AGVList.Last().ID + 1;
            }
            AGVList.Add(new SimulatedAGV(this, newID, name ?? $"AGV{newID:D3}", node));
            return AGVList.Last();
        }

        public void Abort()
        {
            _cts.Cancel();
            foreach (var agv in this.AGVList)
            {
                agv.Disconnect();
            }
        }

        private async void AGVHandling()
        {
            while (!_cts.IsCancellationRequested)
            {
                //DetectDeadLock();
                await Task.Delay(100);
            }
        }             
    }
}
