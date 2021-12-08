using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;
using A_Star_Demo.Maps;
using A_Star_Demo.Models;


namespace A_Star_Demo.AGVs
{
    public class SimulatedAGV
    {
        #region Enums
        [Flags]
        public enum AGVStates
        {
            Idle = 0,
            Moving,                             // AGV is moving.
            Rotating,                           // AGV is rotating.
            RotatingRack,                       // AGV is rotating rack.
            DockingRack,                        // AGV is docking with rack.
            UnDockingRack,                      // AGV is undocking with rack.
            DockingChargeStation,               // AGV is docking with charging station.
            UnDockingChargeStation,             // AGV is undocking with charging staiton.
            Charging,                           // AGV is charging.
            ChargeFinished,                     // AGV has finished charging.

            Disconnected = 100,                 // AGV is disconnected
            Blocked,                            // AGV is blocked by obstacle or other AGVs
            UnknownError,                       // AGV encounters unknown error
        }

        public enum AGVHeading : int
        {
            Up = 90,
            Down = -90,
            Left = 180,
            Right = 0
        }
        #endregion
        public int ID { get; }
        public string Name { get; private set; }
        public MapNode Node { get; private set; }

        public AGVStates State { get; private set; }

        private AGVHeading _heading;
        public AGVHeading Heading
        {
            get
            {
                return _heading;
            }
            private set
            {
                _heading = value;
                _heading = (AGVHeading)((int)_heading % 360);
                if ((int)_heading > 180) _heading -= 180;
                else if ((int)_heading <= -180) _heading += 180;
            }
        }

        public List<MapNode> AssignedPath { get; private set; }

        private readonly CancellationTokenSource _cts;


        public SimulatedAGV(int id, string name, MapNode node)
        {
            this.ID = id;
            this.Name = name;
            this.Node = node;
            this.State = AGVStates.Idle;
            this.Heading = AGVHeading.Right;
            _cts = new CancellationTokenSource();
            Task.Run(() => AGVSimulation(_cts.Token));
        }

        public void AssignNewPath(List<MapNode> path)
        {
            if (path == null) return;
            AssignedPath = new List<MapNode>(path);
        }

        public void Destroy()
        {
            _cts.Cancel();
        }

        private async void AGVSimulation(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                var nextNode = AssignedPath?.FirstOrDefault();
                switch (this.State)
                {
                    case AGVStates.Idle:
                        if (nextNode != null)
                        {
                            if (this.Node == nextNode)
                            {
                                this.State = AGVStates.Moving;
                                AssignedPath.RemoveAt(0);
                            }
                            else
                            {
                                this.State = AGVStates.UnknownError;
                                AssignedPath.Clear();
                            }
                        }
                        break;
                    case AGVStates.Moving:
                        if (nextNode != null)
                        {
                            if (this.Heading == GetNextHeading(this.Node, nextNode))
                            {
                                this.Node = nextNode;
                                AssignedPath.RemoveAt(0);
                            }
                            else
                            {
                                this.State = AGVStates.Rotating;
                            }
                        }
                        else
                        {
                            this.State = AGVStates.Idle;
                        }
                        break;
                    case AGVStates.Rotating:
                        var nextHeading = GetNextHeading(this.Node, nextNode);
                        if (this.Heading == nextHeading)
                        {
                            this.State = AGVStates.Moving;
                        }
                        else
                        {
                            if (Math.Abs(nextHeading - this.Heading) == 180)
                            {
                                this.Heading += 90;
                            }
                            else
                            {
                                this.Heading = nextHeading;
                            }
                        }
                        break;
                }
                Debug.WriteLine($"{this.Name}: State = {this.State}, Heading = {this.Heading}");
                await Task.Delay(100);
            }
            this.State = AGVStates.Disconnected;
        }
        private AGVHeading GetNextHeading(MapNode currentNode, MapNode nextNode)
        {
            int rawHeading = (int)(Math.Atan2(-(nextNode.Location.Y - currentNode.Location.Y), nextNode.Location.X - currentNode.Location.X) / Math.PI * 180.0);
            return (AGVHeading)rawHeading;
        }
    }
}
