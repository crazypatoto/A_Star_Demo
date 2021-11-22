using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using A_Star_Demo.Models;


namespace A_Star_Demo
{
    public class Map
    {
        [Flags]
        public enum EdgeConstraints : byte
        {
            NoConstraints = 0x00,
            NoLeaving = 0x01,
            NoEntering = 0x02,
            NoEnterOrLeaving = 0x03
        }
        public byte ZoneID { get; private set; }
        public string SerialNumber { get; private set; }
        public int Width { get; }
        public int Height { get; }
        public MapNode[,] AllNodes { get; private set; }

        public List<EdgeConstraints[,]> ConstraintLayers;
        public EdgeConstraints[,] AllEdges { get; private set; }
        public Map(byte zoneID, int width, int height)
        {
            this.ZoneID = zoneID;
            this.Width = width;
            this.Height = height;
            this.SerialNumber = Guid.NewGuid().ToString();
            InitializeNewMap();
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
                for (int y = 0; y < Height; y++)
                {
                    for (int x = 0; x < Width; x++)
                    {
                        this.AllNodes[y, x] = new MapNode(binaryReader.ReadBytes(MapNode.RawBytesLength));
                    }
                }
                AllEdges = new EdgeConstraints[2 * Height - 1, Width];
                for (int y = 0; y < 2 * Height - 1; y++)
                {
                    for (int x = 0; x < Width; x++)
                    {
                        this.AllEdges[y, x] = (EdgeConstraints)binaryReader.ReadByte();
                    }
                }
            }
        }

        private void InitializeNewMap()
        {
            this.AllNodes = new MapNode[Height, Width];
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    this.AllNodes[y, x] = new MapNode(ZoneID, x, y);
                }
            }

            this.AllEdges = new EdgeConstraints[2 * Height - 1, Width];
            for (int y = 0; y < 2 * Height - 1; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    this.AllEdges[y, x] = EdgeConstraints.NoConstraints;
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

        public void SaveMap(string path)
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
                for (int y = 0; y < 2 * this.Height - 1; y++)
                {
                    for (int x = 0; x < this.Width; x++)
                    {
                        binaryWriter.Write((byte)this.AllEdges[y, x]);
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

        public EdgeConstraints GetEdgeConstraintsByNodes(MapNode n1, MapNode n2)
        {
            if (this.IsNodeInMap(n1) && this.IsNodeInMap(n2) && n1.IsNeighbourNode(n2))
            {
                if ((n1.Location.X + n1.Location.Y) < (n2.Location.X + n2.Location.Y))
                {
                    return this.AllEdges[n1.Location.Y + n2.Location.Y, (n1.Location.X + n2.Location.X) / 2];
                }
                else
                {
                    var constraints = this.AllEdges[n1.Location.Y + n2.Location.Y, (n1.Location.X + n2.Location.X) / 2];
                    var constraintsFlip = (EdgeConstraints)((((byte)constraints & 0x01) << 1) | (((byte)constraints & 0x02) >> 1));
                    return constraintsFlip;
                }

            }
            else
            {
                throw new Exception("No such edge in map");
            }
        }

        public void SetEdgeConstraintsByNodes(MapNode n1, MapNode n2, EdgeConstraints constraints)
        {
            if (this.IsNodeInMap(n1) && this.IsNodeInMap(n2) && n1.IsNeighbourNode(n2))
            {
                if ((n1.Location.X + n1.Location.Y) < (n2.Location.X + n2.Location.Y))
                {
                    this.AllEdges[n1.Location.Y + n2.Location.Y, (n1.Location.X + n2.Location.X) / 2] = constraints;                    
                }
                else
                {
                    var constraintsFlip = (((byte)constraints & 0x01) > 0 ? 0x02 : 0x00) + (((byte)constraints & 0x02) > 0 ? 0x01 : 0x00);
                    this.AllEdges[n1.Location.Y + n2.Location.Y, (n1.Location.X + n2.Location.X) / 2] = (Map.EdgeConstraints)constraintsFlip;                    
                }
            }
            else
            {
                throw new Exception("No such edge in map");
            }
        }
    }
}
