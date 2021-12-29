using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using A_Star_Demo.Maps;
using A_Star_Demo.Models;
using A_Star_Demo.Tasks;

namespace A_Star_Demo.AGVs
{
    public abstract class AGV : IEquatable<AGV>
    {
        #region Enums
        [Flags]
        public enum AGVStates
        {
            Idle = 0,
            Moving,                             // AGV is moving.
            WaitingToMove,                      // AGV is waiting to move.
            Rotating,                           // AGV is rotating.            
            RotatingRack,                       // AGV is rotating rack.
            WaitingToRotateRack,                // AGV is waiting to rotate rack.
            DockingRack,                        // AGV is docking with rack.
            UnDockingRack,                      // AGV is undocking with rack.
            DockingChargeStation,               // AGV is docking with charging station.
            UnDockingChargeStation,             // AGV is undocking with charging staiton.
            Charging,                           // AGV is charging.
            ChargeFinished,                     // AGV has finished charging.

            Disconnected = 100,                 // AGV is disconnected.
            MovingBlocked,                      // AGV is blocked by obstacle or other AGVs while moving.
            RackRotatingBlocked,                // AGV is blocked by obstacle or other AGVs while rotating rack.
            UnknownError,                       // AGV encounters unknown error.
        }

        public enum AGVHeading : int
        {
            Up = 90,
            Down = -90,
            Left = 180,
            Right = 0
        }
        #endregion
        public AGVHandler Handler { get; }
        public AGVTaskHandler TaskHandler { get; }
        public int ID { get; private protected set; }
        public string Name { get; private protected set; }
        public abstract MapNode CurrentNode { get; private protected set; }
        public Rack BoundRack { get; private protected set; }
        public AGVStates State { get; private protected set; }

        public AGVHeading Heading
        {
            get
            {
                return _heading;
            }
            private protected set
            {
                _heading = value;
                _heading = (AGVHeading)((int)_heading % 360);
                if ((int)_heading > 180) _heading -= 180;
                else if ((int)_heading <= -180) _heading += 180;
            }
        }
           
        protected AGV (AGVHandler handler)
        {
            this.Handler = handler;
            this.TaskHandler = new AGVTaskHandler(this);
        }
        public abstract void StartNewPath(List<MapNode> path, AGVHeading? initialHeading = null);
        public abstract void AddNodeToPath(MapNode node);
        public abstract void EndPath();

        public abstract void PickUpRack(Rack rack);
        public abstract void DropOffRack();

        public abstract void RotateRack(Rack.RackHeading rackHeading);
        public abstract void Disconnect();
        private AGVHeading _heading;

        public bool Equals(AGV other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(this.Name, other.Name);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((AGV)obj);
        }

        public static bool operator ==(AGV left, AGV right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(AGV left, AGV right)
        {
            return !Equals(left, right);
        }

        public override int GetHashCode()
        {
            return (this.Name != null ? this.Name.GetHashCode() : 0);
        }
    }
}
