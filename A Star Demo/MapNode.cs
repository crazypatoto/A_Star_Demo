using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using A_Star_Demo.Models;

namespace A_Star_Demo
{
    public class MapNode
    {
        #region Enums        
        public enum Types : byte
        {
            None,
            ChargingStation,
            WorkStation,
            Storage,    
            Wall,
        }

        [Flags]
        public enum RoutingConstraints : byte
        {
            NoConstraints = 0b00000000,
            NoEnterFromTop = 0b00000001,
            NoEnterFromBottom = 0b00000010,
            NoEnterFromLeft = 0b00000100,
            NoEnterFromRight = 0b00001000,
            NoLeaveFromTop = 0b00010000,
            NoLeaveFromBottom = 0b00100000,
            NoLeaveFromLeft = 0b01000000,
            NoLeaveFromRight = 0b10000000,
            NoEnters = 0b00001111,
            NoExits = 0b11110000,
            NoConnections = 0b11111111
        }
        
        #endregion

        /// <summary>
        /// Zone ID (0~99).
        /// </summary>
        public byte ZoneID { get; private set; }
        /// <summary>
        /// Location of node.
        /// </summary>
        public Location Location { get; private set; }
        public Types Type { get; set; } = Types.None;
        /// <summary>
        /// Node name in format of ZoneID-X-Y (00-000-000).
        /// </summary>
        public string Name
        {
            get { return $"{ZoneID.ToString("D2")}-{Location.X.ToString("D3")}-{Location.Y.ToString("D3")}"; }
        }

        public RoutingConstraints DefaultRoutingConstraints { get; set; } = RoutingConstraints.NoConstraints;
        public RoutingConstraints RackRoutingConstraints { get; set; } = RoutingConstraints.NoConstraints;                        

        public MapNode(byte zoneID, int x, int y)
        {
            ZoneID = zoneID;
            Location = new Location(x, y);
        }

        public void ChangeZoneID(byte newZoneID)
        {
            this.ZoneID = newZoneID;
        }
    }
}
