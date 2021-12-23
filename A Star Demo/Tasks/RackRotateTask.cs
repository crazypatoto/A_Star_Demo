using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using A_Star_Demo.AGVs;
using A_Star_Demo.Models;

namespace A_Star_Demo.Tasks
{
    public class RackRotateTask : AGVTask
    {
        public enum TaskStatus
        {
            Initialized,
            Rotating,
            Waiting,
            Done,
        }
        public Rack.RackHeading TargetHeading { get; }
        public TaskStatus Status { get; set; }
        public RackRotateTask(AGVTaskHandler handler, Rack.RackHeading targetHeading) : base(handler)
        {
            this.TargetHeading = targetHeading;
            this.Status = TaskStatus.Initialized;
        }
        public override void Execute()
        {
            if (this.Status == TaskStatus.Initialized)
            {
                if (this.AssignedAGV.BoundRack == null)
                {
                    this.Finished = true;
                    this.FinishTimeStamp = DateTimeOffset.Now.ToUnixTimeSeconds();
                    return;
                }
                this.AssignedAGV.RotateRack(TargetHeading);
            }
            if (this.AssignedAGV.State == AGV.AGVStates.RotatingRack)
            {
                this.Status = TaskStatus.Rotating;
            }
            if (this.Status == TaskStatus.Rotating && this.AssignedAGV.State == AGV.AGVStates.Idle)
            {
                this.Status = TaskStatus.Done;
                this.Finished = true;
                this.FinishTimeStamp = DateTimeOffset.Now.ToUnixTimeSeconds();
            }
        }
    }
}
