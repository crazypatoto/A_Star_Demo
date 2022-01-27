using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VCS.Models;

namespace VCS.Maps
{
    public class MapNode
    {
        #region Enums        
        /// <summary>
        /// Enums of node types.
        /// </summary>
        public enum Types : byte
        {
            None = 0x00,
            ChargingStation,
            ChargingStationDock,
            WorkStation,
            WorkStationArea,
            WorkStationPickUp,
            Storage,
            Wall,
        }
        #endregion

        public static Dictionary<MapNode.Types, Color> NodeTypeColorDict = new Dictionary<MapNode.Types, Color> {
            {MapNode.Types.None, Color.White },
            {MapNode.Types.Storage, Color.Orange },
            {MapNode.Types.ChargingStation, Color.Yellow },
            {MapNode.Types.ChargingStationDock, Color.YellowGreen },
            {MapNode.Types.WorkStationArea, Color.LightBlue},
            {MapNode.Types.WorkStation, Color.DeepSkyBlue},
            {MapNode.Types.WorkStationPickUp, Color.CadetBlue},
            {MapNode.Types.Wall, Color.DarkGray },
        };

        /// <summary>
        /// Bytes count of raw node data.
        /// </summary>
        public const int RawBytesLength = 11;

        /// <summary>
        /// Zone ID (0~99).
        /// </summary>
        public byte ZoneID { get; private set; }

        /// <summary>
        /// Location of the node.
        /// </summary>
        public Location Location { get; }

        /// <summary>
        /// Type of the node.
        /// </summary>
        public Types Type { get; set; } = Types.None;

        /// <summary>
        /// Disallow turning actions on current node.
        /// </summary>
        public bool DisallowWaitingOnNode { get; set; } = false;

        /// <summary>
        /// Node name in format of ZoneID-X-Y (00-000-000).
        /// </summary>
        public string Name
        {
            get { return $"{ZoneID.ToString("D2")}-{Location.X.ToString("D3")}-{Location.Y.ToString("D3")}"; }
        }

        #region Constructors

        public MapNode(byte zoneID, int x, int y)
        {
            ZoneID = zoneID;
            Location = new Location(x, y);
        }

        public MapNode(byte[] byteArray, int index = 0)
        {
            ZoneID = byteArray[index + 0];
            Location = new Location(BitConverter.ToInt32(byteArray, index + 1), BitConverter.ToInt32(byteArray, index + 5));
            Type = (Types)byteArray[index + 9];
            DisallowWaitingOnNode = Convert.ToBoolean(byteArray[index + 10]);
        }
        #endregion

        public void ChangeZoneID(byte newZoneID)
        {
            this.ZoneID = newZoneID;
        }

        public byte[] ToBytes()
        {
            byte[] byteArray = new byte[RawBytesLength];
            byteArray[0] = ZoneID;
            Array.Copy(BitConverter.GetBytes(Location.X), 0, byteArray, 1, 4);
            Array.Copy(BitConverter.GetBytes(Location.Y), 0, byteArray, 5, 4);
            byteArray[9] = (byte)Type;
            byteArray[10] = Convert.ToByte(DisallowWaitingOnNode);
            return byteArray;
        }

        public bool IsNeighbourNode(MapNode node)
        {
            return Math.Abs(this.Location.X - node.Location.X) + Math.Abs(this.Location.Y - node.Location.Y) == 1;
        }

        public override string ToString()
        {
            return this.Name;
        }
    }
}
