﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
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
        public LinkedList<AGV>[,] AGVNodeQueue { get; }

        public bool IsAlive { get; private set; }


        public VCSServer(Map map)
        {
            CurrentMap = map;
            AGVHandler = new AGVHandler(this);
            PathPlanner = new AStarPlanner(this);
            RackList = new List<Rack>();
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

        public bool IsDeadlockExist()
        {            
            LinkedList<AGV>[] adjacencyList = new LinkedList<AGV>[this.AGVHandler.AGVList.Count];
            for (int i = 0; i < adjacencyList.Length; i++)
            {
                adjacencyList[i] = new LinkedList<AGV>();
            }
            foreach (var agvNodeQueue in this.AGVNodeQueue)
            {
                if (agvNodeQueue.Count < 2) continue;
                var current = agvNodeQueue.First;
                while (current != null)
                {
                    if (current.Next != null)
                    {
                        if (!adjacencyList[current.Value.ID].Contains(current.Next.Value))
                        {
                            adjacencyList[current.Value.ID].AddLast(current.Next.Value);
                        }
                    }
                    current = current.Next;
                }
            }

            bool[] visited = new bool[this.AGVHandler.AGVList.Count];
            bool[] recStack = new bool[this.AGVHandler.AGVList.Count];

            for (int i = 0; i < this.AGVHandler.AGVList.Count; i++)
                if (isCyclicUtil(i))
                    return true;

            bool isCyclicUtil(int id)
            {
                // Mark the current node as visited and 
                // part of recursion stack 
                if (recStack[id])
                    return true;

                if (visited[id])
                    return false;

                visited[id] = true;

                recStack[id] = true;

                foreach (var agv in adjacencyList[id])
                    if (isCyclicUtil(agv.ID))
                        return true;

                recStack[id] = false;

                return false;
            }
            return false;
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
