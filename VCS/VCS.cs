using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Threading;
using VCS.AGVs;
using VCS.Maps;
using VCS.Models;
using VCS.PathPlanning;
using VCS.Tasks;
using VCS.Communication;
using VCS.Missions;

namespace VCS
{
    public class VCS : IDisposable
    {
        private VCSServer _vcsServer;
        private bool _disposed = false;
        public Map CurrentMap { get; }
        public AGVHandler AGVHandler { get; }
        public AStarPlanner PathPlanner { get; }
        public List<Rack> RackList { get; }
        public byte[,] OccupancyGrid { get; }
        public LinkedList<AGV>[,] AGVNodeQueue { get; }
        public MissionHandler MissionHandler { get; }
        
        public bool IsAlive { get; private set; }


        public VCS(Map map)
        {
            this.IsAlive = true;
            this.CurrentMap = map;
            this.AGVHandler = new AGVHandler(this);
            this.PathPlanner = new AStarPlanner(this);
            this.RackList = new List<Rack>();
            this.MissionHandler = new MissionHandler(this);
            OccupancyGrid = new byte[map.Height, map.Width];
            AGVNodeQueue = new LinkedList<AGV>[map.Height, map.Width];
            for (int y = 0; y < map.Height; y++)
            {
                for (int x = 0; x < map.Width; x++)
                {
                    OccupancyGrid[y, x] = 0x00;
                    AGVNodeQueue[y, x] = new LinkedList<AGV>();
                }
            }
            _vcsServer = new VCSServer(this);            
        }

        ~VCS()
        {
            Dispose(false);
        }

        public void AddNewRackTemp(MapNode targetNode)
        {
            var newRackID = this.RackList.LastOrDefault()?.ID + 1 ?? 0;
            var newRack = new Rack(newRackID, targetNode, Rack.RackHeading.Up);
            this.RackList.Add(newRack);
            OccupancyGrid[targetNode.Location.Y, targetNode.Location.X] |= 0x02;
        }
        public void AddNewSimulationAGVTemp(MapNode targetNode)
        {
            AGVHandler.AddSimulatedAGV(targetNode);
            OccupancyGrid[targetNode.Location.Y, targetNode.Location.X] |= 0x01;
        }
        public void SendRackTo(MapNode tagetNode, Rack targetRack, Rack.RackHeading rackHeading)
        {
            AGV selectedAGV = null;
            int minPathLength = int.MaxValue;
            foreach (var agv in this.AGVHandler.AGVList)
            {
                if (agv.State != AGV.AGVStates.Idle) continue;
                var path = this.PathPlanner.FindPath(targetRack.CurrentNode, agv.CurrentNode);
                if (path == null) continue;
                var length = path.Count();
                if (length < minPathLength)
                {
                    minPathLength = length;
                    selectedAGV = agv;
                }
            }
            if (selectedAGV != null)
            {
                selectedAGV.TaskHandler.NewAGVMoveTask(targetRack.CurrentNode);
                selectedAGV.TaskHandler.NewRackPickUpTask(targetRack);
                selectedAGV.TaskHandler.NewAGVMoveTask(tagetNode);
                selectedAGV.TaskHandler.NewRackRotateTask(rackHeading);
                selectedAGV.TaskHandler.NewRackDropOffTask();
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;
            if (disposing)
            {
                this.AGVHandler.Abort();
                this.IsAlive = false;
            }
            _disposed = true;
        }
    }
}
