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
    public class SimulatedAGV : AGV
    {        
        private AGVHeading? _targetHeading = null;
        private readonly CancellationTokenSource _cts;

        public SimulatedAGV(int id, string name, MapNode node)
        {
            this.ID = id;
            this.Name = name;
            this.CurrentNode = node;
            this.State = AGVStates.Idle;
            this.Heading = AGVHeading.Right;
            _cts = new CancellationTokenSource();
            Task.Run(() => AGVSimulation(_cts.Token));
        }

        public override void AssignNewPathAndMove(List<MapNode> path)
        {
            if (path == null) return;
            if (path.Count <= 1) return;
            if (path.First() != this.CurrentNode) return;
            this.AssignedPath = new List<MapNode>(path);
            this.AssignedPath.RemoveAt(0);
            _targetHeading = GetNextHeading(this.CurrentNode, this.AssignedPath.First());
            if (this.Heading == _targetHeading)
            {
                this.State = AGVStates.Moving;
            }
            else
            {
                this.State = AGVStates.Rotating;
            }
        }

        public override void Disconnect()
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
                        break;
                    case AGVStates.Moving:
                        if (nextNode != null)
                        {
                            _targetHeading = GetNextHeading(this.CurrentNode, nextNode);
                            if (this.Heading == _targetHeading)
                            {
                                this.CurrentNode = nextNode;
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
                        if (_targetHeading == null)
                        {
                            this.State = AGVStates.UnknownError;
                        }
                        else
                        {
                            if (this.Heading == _targetHeading)
                            {
                                this.State = AGVStates.Moving;
                            }
                            else
                            {
                                if (Math.Abs((int)(_targetHeading - this.Heading)) == 180)
                                {
                                    this.Heading += 90;
                                }
                                else
                                {
                                    this.Heading = (AGVHeading)_targetHeading;
                                }
                            }
                        }
                        break;
                }                
                await Task.Delay(200);
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
