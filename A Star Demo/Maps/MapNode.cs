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
        /// <summary>
        /// Enums of node types.
        /// </summary>
        public enum Types : byte
        {
            None,
            ChargingStation,
            WorkStation,
            Storage,
            Wall,
        }

        #endregion

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
        public bool DisallowTurningOnNode { get; set; } = false;

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
            DisallowTurningOnNode = Convert.ToBoolean(byteArray[index + 10]);
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
            byteArray[10] = Convert.ToByte(DisallowTurningOnNode);
            return byteArray;
        }

        public bool IsNeighbourNode(MapNode node)
        {
            return Math.Abs(this.Location.X - node.Location.X) + Math.Abs(this.Location.Y - node.Location.Y) == 1;
        }
    }
}
