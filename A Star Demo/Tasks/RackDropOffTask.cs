using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using A_Star_Demo.AGVs;
using A_Star_Demo.Models;

namespace A_Star_Demo.Tasks
{
    public class RackDropOffTask : AGVTask
    {
        public enum TaskStatus
        {
            Initialized,
            UnDocking,
            Done,
        }        
        public TaskStatus Status { get; set; }
        public RackDropOffTask(AGVTaskHandler handler) : base(handler)
        {
            this.Status = TaskStatus.Initialized;
        }
        public override void Execute()
        {
            if(this.Status == TaskStatus.Initialized)
            {
                if (this.AssignedAGV.BoundRack == null)
                {
                    throw new ApplicationException("No rack to drop!");
                }
                this.AssignedAGV.DropOffRack();
            }
            if (this.AssignedAGV.State == AGV.AGVStates.UnDockingRack)
            {
                this.Status = TaskStatus.UnDocking;
            }
            if (this.Status == TaskStatus.UnDocking && this.AssignedAGV.State == AGV.AGVStates.Idle)
            {
                this.Status = TaskStatus.Done;
                this.Finished = true;
                this.FinishTimeStamp = DateTimeOffset.Now.ToUnixTimeSeconds();
            }
        }
    }
}
