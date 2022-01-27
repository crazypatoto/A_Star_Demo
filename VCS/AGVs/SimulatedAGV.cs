using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;
using VCS.Maps;
using VCS.Models;



namespace VCS.AGVs
{
    public class SimulatedAGV : AGV
    {
        private ReaderWriterLockSlim _pathLock = new ReaderWriterLockSlim();
        private List<MapNode> _path;
        public List<MapNode> AssignedPath
        {
            get
            {
                _pathLock.EnterReadLock();
                try
                {
                    return _path;
                }
                finally
                {
                    _pathLock.ExitReadLock();
                }
            }
        }

        public override MapNode CurrentNode { get; private protected set; }
        private AGVHeading? _targetAGVHeading = null;
        private Rack.RackHeading? _targetRackHeading = null;
        private Rack _targetRack = null;
        private bool _pathEndFlag = false;
        private readonly CancellationTokenSource _cts;
        public SimulatedAGV(AGVHandler handler, int id, string name, MapNode node) : base(handler)
        {
            this.ID = id;
            this.Name = name;
            this.CurrentNode = node;
            this.State = AGVStates.Idle;
            this.Heading = AGVHeading.Right;
            _cts = new CancellationTokenSource();
            Task.Run(AGVSimulation);
            //var t = new Thread(() => { AGVSimulation(_cts.Token); });
            //t.IsBackground = true;
            //t.Start();
        }

        public override void StartNewPath(List<MapNode> path, AGVHeading? initialHeading)
        {
            if (this.State != AGVStates.Idle && this.State != AGVStates.MovingBlocked) return;
            if (path == null) return;
            if (path.Count < 1) return;
            if (path.First() != this.CurrentNode) return;
            _pathEndFlag = false;
            _pathLock.EnterWriteLock();
            try
            {
                _path = new List<MapNode>(path);
                _path.RemoveAt(0);
            }
            finally
            {
                _pathLock.ExitWriteLock();
            }
            var nextNode = _path.FirstOrDefault();
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
            _pathLock.EnterWriteLock();
            try
            {
                _path.Add(node);
            }
            finally
            {
                _pathLock.ExitWriteLock();
            }
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
            if (this.State != AGVStates.Idle && this.State != AGVStates.WaitingToRotateRack) return;
            if (this.BoundRack == null) return;
            if (this.BoundRack.Heading == rackHeading) return;
            _targetRackHeading = rackHeading;
            this.State = AGVStates.RotatingRack;
        }
        public override void WaitToRotateRack()
        {
            if (this.State != AGVStates.Idle) return;
            if (this.BoundRack == null) return;
            this.State = AGVStates.WaitingToRotateRack;
        }

        public override void WaitForUserResume()
        {
            if (this.State != AGVStates.Idle) return;
            this.State = AGVStates.WaitingUserResume;
        }

        public override void UserResume()
        {
            if (this.State != AGVStates.WaitingUserResume) return;
            this.State = AGVStates.Idle;
        }
        public override void Disconnect()
        {
            _cts.Cancel();
            this.TaskHandler.Abort();
            this.State = AGVStates.Disconnected;
        }

        private async void AGVSimulation()
        {
            while (!_cts.Token.IsCancellationRequested)
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
                                if (this.Handler.AGVList.FindAll(agv => agv.CurrentNode == nextNode).Count == 0)
                                {
                                    if (this.BoundRack == null)
                                    {
                                        this.CurrentNode = nextNode;
                                        _pathLock.EnterWriteLock();
                                        try
                                        {
                                            _path.RemoveAt(0);
                                        }
                                        finally
                                        {
                                            _pathLock.ExitWriteLock();
                                        }
                                    }
                                    else
                                    {
                                        if (this.Handler.VCS.RackList.FindAll(rack => rack.CurrentNode == nextNode).Count == 0)
                                        {
                                            this.CurrentNode = nextNode;
                                            this.BoundRack.MoveTo(nextNode);
                                            _pathLock.EnterWriteLock();
                                            try
                                            {
                                                _path.RemoveAt(0);
                                            }
                                            finally
                                            {
                                                _pathLock.ExitWriteLock();
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
                                    this.State = AGVStates.MovingBlocked;
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
                                if (this.Handler.VCS.RackList.FindAll(rack => rack.CurrentNode == nextNode).Count == 0)
                                {
                                    this.State = AGVStates.Moving;
                                }
                            }
                        }
                        throw new ApplicationException($"{this.Name} is blocked while moving at {this.CurrentNode.Name}");
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
                            foreach (var neighbor in this.Handler.VCS.CurrentMap.GetNeighborNodes(this.CurrentNode))
                            {
                                if (this.Handler.VCS.RackList.FindAll(rack => rack.CurrentNode == neighbor).Count > 0)
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
                        var neighborNodes = this.Handler.VCS.CurrentMap.GetNeighborNodes(this.CurrentNode);
                        foreach (var neighbor in neighborNodes)
                        {
                            if (this.Handler.VCS.RackList.FindAll(rack => rack.CurrentNode == neighbor).Count > 0)
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
                        throw new ApplicationException($"{this.Name} is blocked while rotating rack at {this.CurrentNode.Name}");
                        break;
                    case AGVStates.WaitingToMove:
                        if (nextNode != null) this.State = AGVStates.Moving;
                        else if (_pathEndFlag) this.State = AGVStates.Idle;
                        break;
                    case AGVStates.WaitingToRotateRack:
                        break;
                }
                await Task.Delay(100);
            }
        }
        private AGVHeading GetNextHeading(MapNode currentNode, MapNode nextNode)
        {
            int rawHeading = (int)(Math.Atan2(-(nextNode.Location.Y - currentNode.Location.Y), nextNode.Location.X - currentNode.Location.X) / Math.PI * 180.0);
            return (AGVHeading)rawHeading;
        }
    }
}
