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
            Failed,
            Done,
        }
        public Rack.RackHeading TargetHeading { get; }
        public TaskStatus Status { get; set; }
        public RackRotateTask(AGVTaskHandler handler, Rack.RackHeading targetHeading) : base(handler)
        {         
            targetHeading = (Rack.RackHeading)((int)targetHeading % 360);
            if ((int)targetHeading > 180) targetHeading -= 360;
            else if ((int)targetHeading <= -180) targetHeading += 360;
            this.TargetHeading = targetHeading;
            this.Status = TaskStatus.Initialized;
        }
        public override void Execute()
        {
            lock (AGVTask._agvTaskLock)
            {
                var vcs = this.AssignedAGV.Handler.VCS;
                var currentNode = this.AssignedAGV.CurrentNode;
                var neighborNodes = vcs.CurrentMap.GetNeighborNodes(currentNode);
                bool readyToRotate = true;

                switch (this.Status)
                {
                    case TaskStatus.Initialized:
                        if (this.AssignedAGV.BoundRack == null)
                        {
                            this.Status = TaskStatus.Failed;
                            break;
                        }
                        if(this.AssignedAGV.BoundRack.Heading == this.TargetHeading)
                        {
                            this.Status = TaskStatus.Done;
                            this.Finished = true;
                            this.FinishTimeStamp = DateTimeOffset.Now.ToUnixTimeSeconds();
                            break;
                        }                       
                        foreach (var neighborNode in neighborNodes)
                        {
                            var agvNodeQueue = vcs.AGVNodeQueue[neighborNode.Location.Y, neighborNode.Location.X];
                            if(agvNodeQueue.Count > 0)
                            {                                
                                var targetAGV = agvNodeQueue.LastOrDefault((agv) => {
                                    if (agv.CurrentNode == currentNode) return true;
                                    if(agv.TaskHandler.CurrentTask is AGVMoveTask)
                                    {
                                        if (!(agv.TaskHandler.CurrentTask as AGVMoveTask).RemainingPath.Contains(neighborNode)) return true;
                                    }
                                    return false;
                                });
                                if (targetAGV != null)
                                {
                                    agvNodeQueue.AddAfter(agvNodeQueue.Find(targetAGV), this.AssignedAGV);
                                }
                                else
                                {
                                    agvNodeQueue.AddFirst(this.AssignedAGV);
                                }
                            }
                            else
                            {
                                agvNodeQueue.AddLast(this.AssignedAGV);
                            }                                                        
                        }
                        
                        foreach (var neighborNode in neighborNodes)
                        {
                            var agvNodeQueue = vcs.AGVNodeQueue[neighborNode.Location.Y, neighborNode.Location.X];
                            if (agvNodeQueue.FirstOrDefault() != this.AssignedAGV) readyToRotate = false;                            
                        }
                        if (readyToRotate)
                        {
                            this.Status = TaskStatus.Rotating;
                            this.AssignedAGV.RotateRack(this.TargetHeading);
                        }
                        else
                        {
                            this.Status = TaskStatus.Waiting;
                            this.AssignedAGV.WaitToRotateRack();
                        }
                        break;
                    case TaskStatus.Rotating:
                        if(this.AssignedAGV.State == AGV.AGVStates.Idle && this.AssignedAGV.BoundRack.Heading == TargetHeading)
                        {
                            foreach (var neighborNode in neighborNodes)
                            {
                                var agvNodeQueue = vcs.AGVNodeQueue[neighborNode.Location.Y, neighborNode.Location.X];
                                if (agvNodeQueue.FirstOrDefault() == this.AssignedAGV)
                                {
                                    agvNodeQueue.RemoveFirst();
                                }
                                else
                                {
                                    throw new ApplicationException($"Unknown error!! {this.AssignedAGV.Name} shoud be first in {neighborNode.Location} AGVNodeQueue");
                                }
                            }
                            this.Status = TaskStatus.Done;
                            this.Finished = true;
                            this.FinishTimeStamp = DateTimeOffset.Now.ToUnixTimeSeconds();
                        }
                        break;
                    case TaskStatus.Waiting:                        
                        foreach (var neighborNode in neighborNodes)
                        {
                            var agvNodeQueue = vcs.AGVNodeQueue[neighborNode.Location.Y, neighborNode.Location.X];
                            if (agvNodeQueue.FirstOrDefault() != this.AssignedAGV) readyToRotate = false;
                        }
                        if (readyToRotate)
                        {
                            this.Status = TaskStatus.Rotating;
                            this.AssignedAGV.RotateRack(this.TargetHeading);
                        }
                        break;
                    case TaskStatus.Done:
                        break;
                }
            }                               
        }
    }
}
