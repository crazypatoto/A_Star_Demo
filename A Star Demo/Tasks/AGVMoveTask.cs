using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using A_Star_Demo.Maps;
using A_Star_Demo.AGVs;

namespace A_Star_Demo.Tasks
{
    public class AGVMoveTask : AGVTask
    {
        public enum TaskStatus
        {
            Initialzied,
            AssigningPath,
            NoAvailablePathFound,
            Arrived,
        }
        public MapNode Destination { get; private set; }
        public List<MapNode> FullPath { get; private set; }
        public Queue<MapNode> RemainingPath { get; private set; }
        public TaskStatus Status { get; private set; }
        public AGVMoveTask(AGVTaskHandler handler, MapNode destination) : base(handler)
        {
            this.Destination = destination;
            this.Status = TaskStatus.Initialzied;
            if (destination == this.AssignedAGV.CurrentNode)
            {
                this.Status = TaskStatus.Arrived;
                this.Finished = true;
                this.FinishTimeStamp = DateTimeOffset.Now.ToUnixTimeSeconds();
            }
        }
        public override void Execute()
        {
            var vcsServer = AssignedAGV.Handler.VCSServer;
            switch (this.Status)
            {
                case TaskStatus.Initialzied:
                    lock (_agvTaskLock)
                    {
                        this.FullPath = vcsServer.PathPlanner.FindPath(this.AssignedAGV.CurrentNode, this.Destination);
                        if (this.FullPath != null)
                        {
                            for (int i = 1; i < FullPath.Count; i++)
                            {
                                var node = this.FullPath[i];
                                vcsServer.AGVNodeQueue[node.Location.Y, node.Location.X].Enqueue(this.AssignedAGV);
                            }
                            this.RemainingPath = new Queue<MapNode>(this.FullPath);
                            var path = new List<MapNode>();
                            path.Add(RemainingPath.Dequeue());
                            while (this.RemainingPath.Count > 0)
                            {
                                var nextNode = this.RemainingPath.Peek();
                                if (vcsServer.AGVNodeQueue[nextNode.Location.Y, nextNode.Location.X].Peek() == this.AssignedAGV)
                                {
                                    path.Add(RemainingPath.Dequeue());
                                }
                                else
                                {
                                    break;
                                }
                            }
                            this.AssignedAGV.StartNewPath(path);
                            if (this.RemainingPath.Count == 0) this.AssignedAGV.EndPath();
                            this.Status = TaskStatus.AssigningPath;
                            vcsServer.OccupancyGrid[Destination.Location.Y, Destination.Location.X] |= this.AssignedAGV.BoundRack == null ? (byte)0x01 : (byte)0x03;                            
                            vcsServer.OccupancyGrid[this.AssignedAGV.CurrentNode.Location.Y, this.AssignedAGV.CurrentNode.Location.X] &= this.AssignedAGV.BoundRack == null ? (byte)0xFE : (byte)0xFC;
                            // Poor performance, must impove                   
                            foreach (var agv in vcsServer.AGVHandler.AGVList)
                            {
                                if (agv.TaskHandler.CurrentTask == null) continue;
                                if (!(agv.TaskHandler.CurrentTask is AGVMoveTask)) continue;
                                if(((AGVMoveTask)agv.TaskHandler.CurrentTask).Destination == this.AssignedAGV.CurrentNode)
                                {
                                    vcsServer.OccupancyGrid[this.AssignedAGV.CurrentNode.Location.Y, this.AssignedAGV.CurrentNode.Location.X] |= agv.BoundRack == null ? (byte)0x01 : (byte)0x03;
                                }
                            }
                        }
                        else
                        {
                            this.Status = TaskStatus.NoAvailablePathFound;
                            this.Finished = true;
                            this.FinishTimeStamp = DateTimeOffset.Now.ToUnixTimeSeconds();
                        }
                    }
                    break;
                case TaskStatus.AssigningPath:
                    lock (_agvTaskLock)
                    {
                        if (RemainingPath.Count > 0)
                        {
                            var nextNode = RemainingPath.Peek();
                            if (vcsServer.AGVNodeQueue[nextNode.Location.Y, nextNode.Location.X].Peek() == this.AssignedAGV)
                            {
                                this.AssignedAGV.AddNodeToPath(this.RemainingPath.Dequeue());
                                if (this.RemainingPath.Count == 0) this.AssignedAGV.EndPath();
                            }
                        }
                        var currentIndex = this.FullPath.FindIndex(node => node == this.AssignedAGV.CurrentNode);
                        if (currentIndex > 0)
                        {
                            var prevNode = this.FullPath[currentIndex - 1];
                            var targetAGVNodeQueue = vcsServer.AGVNodeQueue[prevNode.Location.Y, prevNode.Location.X];
                            if (targetAGVNodeQueue.Count > 0)
                            {
                                if (targetAGVNodeQueue.Peek() == this.AssignedAGV)
                                {
                                    targetAGVNodeQueue.Dequeue();
                                    //vcsServer.OccupancyGrid[prevNode.Location.Y, prevNode.Location.X] &= 0xFE;
                                }
                            }
                        }
                        if (this.AssignedAGV.CurrentNode == this.Destination)
                        {
                            //vcsServer.AGVNodeQueue[this.Destination.Location.Y, this.Destination.Location.X].Dequeue();
                            vcsServer.OccupancyGrid[Destination.Location.Y, Destination.Location.X] |= this.AssignedAGV.BoundRack == null ? (byte)0x01 : (byte)0x03;
                            this.Finished = true;
                            this.FinishTimeStamp = DateTimeOffset.Now.ToUnixTimeSeconds();
                        }
                    }
                    break;
            }
        }
    }
}
