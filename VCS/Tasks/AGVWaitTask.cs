using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VCS.AGVs;
using VCS.Models;

namespace VCS.Tasks
{
    public class AGVWaitTask : AGVTask
    {
        public enum TaskStatus
        {
            Initialized,
            Waiting,
            Done,
        }        
        public TaskStatus Status { get; set; }
        private Stopwatch stopwatch;
        public AGVWaitTask(AGVTaskHandler handler) : base(handler)
        {         
            this.Status = TaskStatus.Initialized;            
        }
        public override void Execute()
        {
            if (this.Status == TaskStatus.Initialized)
            {               
                this.AssignedAGV.WaitForUserResume();
                stopwatch = new Stopwatch();                
            }
            if (this.AssignedAGV.State == AGV.AGVStates.WaitingUserResume)
            {
                this.Status = TaskStatus.Waiting;
                stopwatch.Start();
            }            
            if (this.Status == TaskStatus.Waiting)
            {
                if (stopwatch.ElapsedMilliseconds > 3000) this.AssignedAGV.UserResume();
                if(this.AssignedAGV.State == AGV.AGVStates.Idle)
                {
                    this.Status = TaskStatus.Done;
                    this.Finished = true;
                    this.FinishTimeStamp = DateTimeOffset.Now.ToUnixTimeSeconds();
                }                
            }
        }
    }
}
