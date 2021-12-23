using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using A_Star_Demo.AGVs;
using A_Star_Demo.Maps;
using A_Star_Demo.Models;
using A_Star_Demo.PathPlanning;
using A_Star_Demo.Tasks;

namespace A_Star_Demo
{
    public class VCSServer : IDisposable
    {
        bool _disposed = false;
        public Map CurrentMap { get; }
        public AGVHandler AGVHandler { get; }
        public AStarPlanner PathPlanner { get; }
        public List<Rack> RackList { get; }
        public byte[,] OccupancyGrid { get; }
        public Queue<AGV>[,] AGVNodeQueue { get; }

        public bool IsAlive { get; private set; }


        public VCSServer(Map map)
        {
            CurrentMap = map;
            AGVHandler = new AGVHandler(this);
            PathPlanner = new AStarPlanner(this);
            RackList = new List<Rack>();
            OccupancyGrid = new byte[map.Height, map.Width];
            AGVNodeQueue = new Queue<AGV>[map.Height, map.Width];
            for (int y = 0; y < map.Height; y++)
            {
                for (int x = 0; x < map.Width; x++)
                {
                    OccupancyGrid[y,x] = 0x00;
                    AGVNodeQueue[y,x] = new Queue<AGV>();                    
                }
            }
            IsAlive = true;
        }

        ~VCSServer()
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
                this.IsAlive = false;
            }
            _disposed = true;
        }
    }
}
