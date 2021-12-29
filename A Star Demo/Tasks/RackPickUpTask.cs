using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using A_Star_Demo.AGVs;
using A_Star_Demo.Models;

namespace A_Star_Demo.Tasks
{
    public class RackPickUpTask : AGVTask
    {
        public enum TaskStatus
        {
            Initialized,
            Docking,
            Done,
        }
        public Rack TargetRack { get; }
        public TaskStatus Status { get; set; }
        public RackPickUpTask(AGVTaskHandler handler, Rack targetRack) : base(handler)
        {
            this.TargetRack = targetRack;
            this.Status = TaskStatus.Initialized;            
        }
        public override void Execute()
        {
            if(this.Status == TaskStatus.Initialized)
            {
                if (this.TargetRack.CurrentNode != this.AssignedAGV.CurrentNode)
                {
                    throw new ApplicationException("Taget rack isn't in current location!");
                }
                this.AssignedAGV.PickUpRack(TargetRack);
            }
            if (this.AssignedAGV.State == AGV.AGVStates.DockingRack)
            {
                this.Status = TaskStatus.Docking;
            }
            if (this.Status == TaskStatus.Docking && this.AssignedAGV.State == AGV.AGVStates.Idle)
            {
                this.Status = TaskStatus.Done;
                this.Finished = true;
                this.FinishTimeStamp = DateTimeOffset.Now.ToUnixTimeSeconds();
            }
        }
    }
}
