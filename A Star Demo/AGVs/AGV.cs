using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using A_Star_Demo.Maps;
using A_Star_Demo.Models;

namespace A_Star_Demo.AGVs
{
    public abstract class AGV
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
        public int ID { get; private protected set; }
        public string Name { get; private protected set; }
        public MapNode CurrentNode { get; private protected set; }
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
        public List<MapNode> AssignedPath { get; private protected set; }

        public abstract void AssignNewPathAndMove(List<MapNode> path);
        public abstract void Disconnect();
        private AGVHeading _heading;
    }
}
