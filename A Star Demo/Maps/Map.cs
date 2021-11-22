using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using A_Star_Demo.Maps;
using A_Star_Demo.Models;


namespace A_Star_Demo
{
    public class Map
    {
        public string SerialNumber { get; private set; }
        public byte ZoneID { get; private set; }
        public int Width { get; }
        public int Height { get; }
        public MapNode[,] AllNodes { get; private set; }

        public List<ConstraintLayer> ConstraintLayers;

        public Map(byte zoneID, int width, int height)
        {
            this.SerialNumber = Guid.NewGuid().ToString();
            this.ZoneID = zoneID;
            this.Width = width;
            this.Height = height;

            this.AllNodes = new MapNode[height, width];
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    this.AllNodes[y, x] = new MapNode(ZoneID, x, y);
                }
            }

            this.ConstraintLayers = new List<ConstraintLayer>();
            this.ConstraintLayers.Add(new ConstraintLayer(this, "Default"));
        }

        public Map(string path)
        {
            using (BinaryReader binaryReader = new BinaryReader(File.OpenRead(path)))
            {
                this.SerialNumber = binaryReader.ReadString();
                this.ZoneID = binaryReader.ReadByte();
                this.Width = binaryReader.ReadInt32();
                this.Height = binaryReader.ReadInt32();
                this.AllNodes = new MapNode[Height, Width];
                for (int y = 0; y < this.Height; y++)
                {
                    for (int x = 0; x < this.Width; x++)
                    {
                        this.AllNodes[y, x] = new MapNode(binaryReader.ReadBytes(MapNode.RawBytesLength));
                    }
                }

                this.ConstraintLayers = new List<ConstraintLayer>();
                var layersCount = binaryReader.ReadByte();
                for (int i = 0; i < layersCount; i++)
                {
                    this.ConstraintLayers.Add(new ConstraintLayer(this, binaryReader.ReadString()));
                    for (int y = 0; y < 2 * this.Height - 1; y++)
                    {
                        for (int x = 0; x < this.Width; x++)
                        {
                            this.ConstraintLayers[i].EdgeConstraints[y, x] = new MapEdge(binaryReader.ReadBytes(MapEdge.RawBytesLength));
                        }
                    }
                }
            }
        }


        public void ChangeZoneID(byte newZoneID)
        {
            this.ZoneID = newZoneID;
            foreach (var node in this.AllNodes)
            {
                node.ChangeZoneID(newZoneID);
            }
        }

        public void SaveToFile(string path)
        {
            using (BinaryWriter binaryWriter = new BinaryWriter(File.Open(path, FileMode.Create)))
            {
                binaryWriter.Write(this.SerialNumber);
                binaryWriter.Write(this.ZoneID);
                binaryWriter.Write(this.Width);
                binaryWriter.Write(this.Height);
                for (int y = 0; y < this.Height; y++)
                {
                    for (int x = 0; x < this.Width; x++)
                    {
                        binaryWriter.Write(this.AllNodes[y, x].ToBytes());
                    }
                }

                binaryWriter.Write((byte)this.ConstraintLayers.Count);
                for (int i = 0; i < this.ConstraintLayers.Count; i++)
                {
                    binaryWriter.Write(this.ConstraintLayers[i].Name);
                    for (int y = 0; y < 2 * this.Height - 1; y++)
                    {
                        for (int x = 0; x < this.Width; x++)
                        {
                            binaryWriter.Write(this.ConstraintLayers[i].EdgeConstraints[y, x].ToBytes());
                        }
                    }
                }
            }
        }

        public bool IsNodeInMap(MapNode node)
        {
            if (node.ZoneID != this.ZoneID) return false;
            if (node.Location.X < 0 || node.Location.Y < 0) return false;
            if (node.Location.X >= this.Width || node.Location.Y >= this.Height) return false;
            return true;
        }

        public MapEdge GetEdgeByNodes(int edgeLayerIndex, MapNode n1, MapNode n2)
        {
            return this.ConstraintLayers[edgeLayerIndex].EdgeConstraints[n1.Location.Y + n2.Location.Y, (n1.Location.X + n2.Location.X) / 2];
        }        

        public List<MapNode> GetNeighborNodes(MapNode node)
        {
            List<MapNode> neighborNodes = new List<MapNode>();
            if (node.Location.X - 1 >= 0) neighborNodes.Add(this.AllNodes[node.Location.Y, node.Location.X - 1]);
            if (node.Location.Y - 1 >= 0) neighborNodes.Add(this.AllNodes[node.Location.Y - 1, node.Location.X]);
            if (node.Location.X + 1 < this.Width) neighborNodes.Add(this.AllNodes[node.Location.Y, node.Location.X + 1]);
            if (node.Location.Y + 1 < this.Height) neighborNodes.Add(this.AllNodes[node.Location.Y + 1, node.Location.X]);
            return neighborNodes;
        }

        public class ConstraintLayer
        {
            public string Name { get; set; }
            public MapEdge[,] EdgeConstraints { get; }

            public ConstraintLayer(Map map, string layerName)
            {
                this.Name = layerName;
                this.EdgeConstraints = new MapEdge[2 * map.Height - 1, map.Width];

                for (int y = 0; y < 2 * map.Height - 1; y++)
                {
                    for (int x = 0; x < map.Width; x++)
                    {
                        this.EdgeConstraints[y, x] = new MapEdge();
                    }
                }
            }
        }

    }
}
