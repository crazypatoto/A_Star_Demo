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
        private AGVHeading? _targetAGVHeading = null;
        private Rack.RackHeading? _targetRackHeading = null;
        private Rack _targetRack = null;
        private bool _pathEndFlag = false;
        private readonly CancellationTokenSource _cts;
        private static readonly object _lock = new object();
        public SimulatedAGV(AGVHandler handler, int id, string name, MapNode node) : base(handler)
        {
            this.ID = id;
            this.Name = name;
            this.CurrentNode = node;
            this.State = AGVStates.Idle;
            this.Heading = AGVHeading.Right;
            _cts = new CancellationTokenSource();
            Task.Run(() => AGVSimulation(_cts.Token));
        }

        public override void StartNewPath(List<MapNode> path, AGVHeading? initialHeading)
        {
            if (this.State != AGVStates.Idle && this.State != AGVStates.MovingBlocked) return;
            if (path == null) return;
            if (path.Count < 1) return;
            if (path.First() != this.CurrentNode) return;
            _pathEndFlag = false;
            this.AssignedPath = new List<MapNode>(path);
            this.AssignedPath.RemoveAt(0);
            var nextNode = this.AssignedPath.FirstOrDefault();
            if (nextNode == null)
            {
                this.State = AGVStates.WaitingToMove;
                if (initialHeading == null) return;
                if (this.Heading == initialHeading) return;
                _targetAGVHeading = initialHeading;
                this.State = AGVStates.Rotating;
            }
            else
            {
                _targetAGVHeading = GetNextHeading(this.CurrentNode, nextNode);
                if (this.Heading == _targetAGVHeading)
                {
                    this.State = AGVStates.Moving;
                }
                else
                {
                    this.State = AGVStates.Rotating;
                }
            }
        }

        public override void AddNodeToPath(MapNode node)
        {
            this.AssignedPath.Add(node);
        }
        public override void EndPath()
        {
            _pathEndFlag = true;
        }

        public override void PickUpRack(Rack rack)
        {
            if (this.State != AGVStates.Idle) return;
            if (this.BoundRack != null) return;
            if (this.CurrentNode != rack.CurrentNode) return;
            _targetRack = rack;
            this.State = AGVStates.DockingRack;
        }

        public override void DropOffRack()
        {
            if (this.State != AGVStates.Idle) return;
            if (this.BoundRack == null) return;
            this.State = AGVStates.UnDockingRack;
        }
        public override void RotateRack(Rack.RackHeading rackHeading)
        {
            if (this.State != AGVStates.Idle) return;
            if (this.BoundRack == null) return;
            if (this.BoundRack.Heading == rackHeading) return;
            _targetRackHeading = rackHeading;
            this.State = AGVStates.RotatingRack;
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
                            _targetAGVHeading = GetNextHeading(this.CurrentNode, nextNode);
                            if (this.Heading == _targetAGVHeading)
                            {
                                lock (_lock)
                                {
                                    if (this.Handler.AGVList.FindAll(agv => agv.CurrentNode == nextNode).Count == 0)
                                    {
                                        if (this.BoundRack == null)
                                        {
                                            this.CurrentNode = nextNode;
                                            AssignedPath.RemoveAt(0);
                                        }
                                        else
                                        {
                                            if (this.Handler.VCSServer.RackList.FindAll(rack => rack.CurrentNode == nextNode).Count == 0)
                                            {
                                                this.CurrentNode = nextNode;
                                                this.BoundRack.MoveTo(nextNode);
                                                AssignedPath.RemoveAt(0);
                                            }
                                            else
                                            {
                                                this.State = AGVStates.MovingBlocked;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        this.State = AGVStates.MovingBlocked;
                                    }
                                }
                            }
                            else
                            {
                                this.State = AGVStates.Rotating;
                            }
                        }
                        else
                        {
                            if (_pathEndFlag)
                            {
                                this.State = AGVStates.Idle;
                            }
                            else
                            {
                                this.State = AGVStates.WaitingToMove;
                            }

                        }
                        break;
                    case AGVStates.Rotating:
                        if (this.Heading == _targetAGVHeading)
                        {
                            this.State = AGVStates.Moving;
                        }
                        else
                        {
                            if (Math.Abs((int)(_targetAGVHeading - this.Heading)) == 180)
                            {
                                this.Heading += 90;
                            }
                            else
                            {
                                this.Heading = (AGVHeading)_targetAGVHeading;
                            }
                        }
                        break;
                    case AGVStates.MovingBlocked:
                        if (this.Handler.AGVList.FindAll(agv => agv.CurrentNode == nextNode).Count == 0)
                        {
                            if (this.BoundRack == null)
                            {
                                this.State = AGVStates.Moving;
                            }
                            else
                            {
                                if (this.Handler.VCSServer.RackList.FindAll(rack => rack.CurrentNode == nextNode).Count == 0)
                                {
                                    this.State = AGVStates.Moving;
                                }
                            }
                        }
                        break;
                    case AGVStates.DockingRack:
                        this.BoundRack = _targetRack;
                        _targetRack = null;
                        this.State = AGVStates.Idle;
                        break;
                    case AGVStates.UnDockingRack:
                        this.BoundRack = null;
                        this.State = AGVStates.Idle;
                        break;
                    case AGVStates.RotatingRack:
                        if (this.BoundRack.Heading == _targetRackHeading)
                        {
                            this.State = AGVStates.Idle;
                        }
                        else
                        {
                            foreach (var neighbor in this.Handler.VCSServer.CurrentMap.GetNeighborNodes(this.CurrentNode))
                            {
                                if (this.Handler.VCSServer.RackList.FindAll(rack => rack.CurrentNode == neighbor).Count > 0)
                                {
                                    this.State = AGVStates.RackRotatingBlocked;
                                    break;
                                }
                                if (this.Handler.AGVList.FindAll(agv => agv.CurrentNode == neighbor).Count > 0)
                                {
                                    this.State = AGVStates.RackRotatingBlocked;
                                    break;
                                }
                            }
                            if (this.State != AGVStates.RackRotatingBlocked)
                            {
                                if (Math.Abs((int)(_targetRackHeading - this.BoundRack.Heading)) == 180)
                                {
                                    this.BoundRack.RotateTo(this.BoundRack.Heading + 90);
                                }
                                else
                                {
                                    this.BoundRack.RotateTo((Rack.RackHeading)_targetRackHeading);
                                }
                            }
                        }
                        break;
                    case AGVStates.RackRotatingBlocked:
                        var neighborNodes = this.Handler.VCSServer.CurrentMap.GetNeighborNodes(this.CurrentNode);
                        foreach (var neighbor in neighborNodes)
                        {
                            if (this.Handler.VCSServer.RackList.FindAll(rack => rack.CurrentNode == neighbor).Count > 0)
                            {
                                this.State = AGVStates.RackRotatingBlocked;
                                break;
                            }
                            if (this.Handler.AGVList.FindAll(agv => agv.CurrentNode == neighbor).Count > 0)
                            {
                                this.State = AGVStates.RackRotatingBlocked;
                                break;
                            }
                            this.State = AGVStates.RotatingRack;
                        }
                        break;
                    case AGVStates.WaitingToMove:
                        if (nextNode != null) this.State = AGVStates.Moving;
                        else if (_pathEndFlag) this.State = AGVStates.Idle;
                        break;
                    case AGVStates.WaitingToRotateRack:
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
