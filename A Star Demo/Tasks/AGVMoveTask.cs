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
        private int _lastDequeuedNodeIndex = -1;
        public MapNode Destination { get; private set; }
        public List<MapNode> FullPath { get; private set; }
        public Queue<MapNode> RemainingPath { get; private set; }
        public TaskStatus Status { get; private set; }
        public AGVMoveTask(AGVTaskHandler handler, MapNode destination) : base(handler)
        {
            this.Destination = destination;
            this.Status = TaskStatus.Initialzied;
        }
        public override void Execute()
        {
            //bool pathNotFound = false;
            lock (_agvTaskLock)
            {
                var vcsServer = AssignedAGV.Handler.VCSServer;
                var AGVCurrentNode = this.AssignedAGV.CurrentNode;
                switch (this.Status)
                {
                    case TaskStatus.Initialzied:
                        if (this.Destination == AGVCurrentNode)
                        {
                            this.Status = TaskStatus.Arrived;
                            this.Finished = true;
                            this.FinishTimeStamp = DateTimeOffset.Now.ToUnixTimeSeconds();
                            break;
                        }

                        this.FullPath = vcsServer.PathPlanner.FindPath(AGVCurrentNode, this.Destination, this.AssignedAGV.BoundRack != null, this.AssignedAGV.BoundRack == null ? 0 : 1);
                        if (this.FullPath != null)
                        {
                            for (int i = 0; i < FullPath.Count; i++)
                            {
                                var node = this.FullPath[i];
                                if (i == 0)
                                {
                                    if (vcsServer.AGVNodeQueue[node.Location.Y, node.Location.X].FirstOrDefault() != this.AssignedAGV)
                                    {
                                        vcsServer.AGVNodeQueue[node.Location.Y, node.Location.X].AddFirst(this.AssignedAGV);
                                    }
                                }                              
                                else
                                {
                                    vcsServer.AGVNodeQueue[node.Location.Y, node.Location.X].AddLast(this.AssignedAGV);
                                }
                            }
                            this.RemainingPath = new Queue<MapNode>(this.FullPath);
                            this.AssignedAGV.StartNewPath(new List<MapNode>() { RemainingPath.Dequeue() });
                            vcsServer.OccupancyGrid[Destination.Location.Y, Destination.Location.X] |= this.AssignedAGV.BoundRack == null ? (byte)0x01 : (byte)0x03;
                            vcsServer.OccupancyGrid[AGVCurrentNode.Location.Y, AGVCurrentNode.Location.X] &= this.AssignedAGV.BoundRack == null ? (byte)0xFE : (byte)0xFC;

                            // Poor performance, must impove                   
                            foreach (var agv in vcsServer.AGVHandler.AGVList)
                            {
                                if (agv.TaskHandler.CurrentTask == null) continue;
                                if (!(agv.TaskHandler.CurrentTask is AGVMoveTask)) continue;
                                if (((AGVMoveTask)agv.TaskHandler.CurrentTask).Destination == AGVCurrentNode)
                                {
                                    vcsServer.OccupancyGrid[AGVCurrentNode.Location.Y, AGVCurrentNode.Location.X] |= agv.BoundRack == null ? (byte)0x01 : (byte)0x03;
                                }
                            }
                            this.Status = TaskStatus.AssigningPath;
                        }
                        else
                        {
                            //pathNotFound = true;
                            System.Diagnostics.Debug.WriteLine($"{this.AssignedAGV.Name} path not found");
                            //throw new ApplicationException("No path found!");
                        }
                        break;
                    case TaskStatus.AssigningPath:

                        if (this.RemainingPath.Count > 0)
                        {
                            var nextNode = RemainingPath.Peek();
                            var nextAGVNodeQueue = vcsServer.AGVNodeQueue[nextNode.Location.Y, nextNode.Location.X];

                            if (this.AssignedAGV.BoundRack == null)
                            {
                                if (nextAGVNodeQueue.FirstOrDefault() == this.AssignedAGV)
                                {
                                    this.AssignedAGV.AddNodeToPath(this.RemainingPath.Dequeue());
                                    //System.Diagnostics.Debug.WriteLineIf(vcsServer.IsDeadlockExist(), "Dead Lock!!!");
                                }
                                else if ((vcsServer.OccupancyGrid[nextNode.Location.Y, nextNode.Location.X] & 0x02) > 0)
                                {
                                    var firstAGVWithoutRack = nextAGVNodeQueue.FirstOrDefault(agv => agv.BoundRack == null);
                                    if (firstAGVWithoutRack == this.AssignedAGV)
                                    {
                                        this.AssignedAGV.AddNodeToPath(this.RemainingPath.Dequeue());
                                        nextAGVNodeQueue.Remove(firstAGVWithoutRack);
                                        nextAGVNodeQueue.AddFirst(firstAGVWithoutRack);
                                    }
                                    else
                                    {
                                        // System.Diagnostics.Debug.WriteLineIf(vcsServer.IsDeadlockExist(), "Dead Lock!!!");
                                    }
                                }
                            }
                            else
                            {
                                // Slow but works, could be optimized in the future.
                                if (nextAGVNodeQueue.FirstOrDefault() == this.AssignedAGV && vcsServer.RackList.FirstOrDefault(rack => rack.CurrentNode == nextNode) == null)
                                {
                                    this.AssignedAGV.AddNodeToPath(this.RemainingPath.Dequeue());
                                }
                                else
                                {
                                    //System.Diagnostics.Debug.WriteLineIf(vcsServer.IsDeadlockExist(), "Dead Lock!!!");
                                }
                            }
                            if (this.RemainingPath.Count == 0) this.AssignedAGV.EndPath();
                        }


                        var currentIndex = this.FullPath.FindIndex(node => node == AGVCurrentNode);
                        for (int i = _lastDequeuedNodeIndex + 1; i < currentIndex; i++)
                        {
                            var passedNode = this.FullPath[i];
                            var targetAGVNodeQueue = vcsServer.AGVNodeQueue[passedNode.Location.Y, passedNode.Location.X];
                            if (targetAGVNodeQueue.Count > 0)
                            {
                                if (targetAGVNodeQueue.First() == this.AssignedAGV)
                                {
                                    targetAGVNodeQueue.RemoveFirst();
                                    _lastDequeuedNodeIndex = i;
                                }
                                else
                                {
                                    System.Diagnostics.Debug.WriteLine($"!!!!!!{this.AssignedAGV.Name} error!!!!!!!! first is {targetAGVNodeQueue.First().Name} at {passedNode.Name}");
                                }
                            }
                        }

                        if (AGVCurrentNode == this.Destination)
                        {
                            //vcsServer.AGVNodeQueue[this.Destination.Location.Y, this.Destination.Location.X].RemoveFirst();
                            vcsServer.OccupancyGrid[Destination.Location.Y, Destination.Location.X] |= this.AssignedAGV.BoundRack == null ? (byte)0x01 : (byte)0x03;
                            this.Finished = true;
                            this.FinishTimeStamp = DateTimeOffset.Now.ToUnixTimeSeconds();
                        }
                        break;
                }
            }
            //if (pathNotFound)
            //{
            //    System.Threading.Thread.Sleep(1000);
            //}
        }

    }
}
