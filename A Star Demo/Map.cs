using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A_Star_Demo
{
    public class Map
    {
        public byte ZoneID { get; private set; }
        public string SerialNumber { get; private set; }
        public int Width { get; }
        public int Height { get; }
        public MapNode[,] AllNodes { get; private set; }
        public Map(byte zoneID, int width, int height)
        {
            ZoneID = zoneID;
            Width = width;
            Height = height;
            SerialNumber = Guid.NewGuid().ToString();
            InitializeMapNodes();
        }

        public Map(string path)
        {

        }

        private void InitializeMapNodes()
        {
            AllNodes = new MapNode[Height, Width];
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    AllNodes[y, x] = new MapNode(ZoneID, x, y);
                }
            }
        }

        public void ChangeZoneID(byte newZoneID)
        {
            ZoneID = newZoneID;
            foreach (var node in AllNodes)
            {
                node.ChangeZoneID(newZoneID);
            }
        }
    }
}
