using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VCS.Models;
using VCS.Maps;
using VCS.AGVs;

namespace VCS.Missions
{
    public class Mission
    {
        public enum MissionState
        {
            Initialized,
            PickingUpRack,
            GoingToDestination,
            RotatingRack,
            WaitingUserResume,
            TakingRackHome,
            Done,
            Error,
        }
        
        public Rack TargetRack { get; }
        public MapNode Destination { get; }
        public Rack.RackHeading TargetRackHeading { get; }
        public AGV AssignedAGV { get; private set; }
        internal MissionState State { get; set; }

        public Mission BoundedMission { get; set; }
        public Mission(Rack rack, MapNode dest, Rack.RackHeading heading)
        {
            this.TargetRack = rack;
            this.Destination = dest;
            this.TargetRackHeading = heading;
            this.TargetRackHeading = (Rack.RackHeading)((int)this.TargetRackHeading % 360);
            if ((int)this.TargetRackHeading > 180) this.TargetRackHeading -= 360;
            else if ((int)this.TargetRackHeading <= -180) this.TargetRackHeading += 360;
        }

        public void AssignAGV(AGV agv)
        {
            this.AssignedAGV = agv;
        }
    }
}
