﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Diagnostics;
using A_Star_Demo.Maps;
using A_Star_Demo.Models;
using A_Star_Demo.AGVs;

namespace A_Star_Demo
{
    public class MapDrawer
    {
        private readonly int _minCellSize = 20;
        private VCSServer _vcsServer;
        private Bitmap _mapBMP;
        private Size _drawSize;
        private int _cellSize;
        private int _scaledCellSize;
        private float _scale;
        public int GridWidth { get; set; } = 3;
        public int OffsetX { get; set; } = 0;
        public int OffsetY { get; set; } = 0;
        public int CellSize { get { return _scaledCellSize; } }

        public float Scale
        {
            get
            {
                return _scale;
            }
            set
            {
                _scale = value;
                _scaledCellSize = (int)(_cellSize * _scale);
                if (_scaledCellSize < _minCellSize)
                {
                    _scaledCellSize = _minCellSize;
                    _scale = (float)_scaledCellSize / _cellSize;
                }
            }
        }
        public Size DrawSize
        {
            get
            {
                return _drawSize;
            }
            set
            {
                _drawSize = value;
                _cellSize = Math.Min(DrawSize.Width / _vcsServer.CurrentMap.Width, DrawSize.Height / _vcsServer.CurrentMap.Height);
                if (_cellSize < _minCellSize) _cellSize = _minCellSize;
                _scale = (float)_scaledCellSize / _cellSize;
            }
        }

        public static Dictionary<MapNode.Types, Color> NodeTypeColorDict = new Dictionary<MapNode.Types, Color> {
            {MapNode.Types.None, Color.White },
            {MapNode.Types.Storage, Color.Orange },
            {MapNode.Types.ChargingStation, Color.Yellow },
            {MapNode.Types.WorkStation, Color.LightBlue},
            {MapNode.Types.Wall, Color.DarkGray },
        };

        public MapDrawer(VCSServer server, Size drawSize)
        {
            _vcsServer = server;
            this.DrawSize = drawSize;
            this.Scale = 1.0f;
        }

        public Bitmap GetMapPicture()
        {
            return _mapBMP;
        }
        public void DrawNewMap()
        {
            _mapBMP?.Dispose();
            GC.Collect();
            _mapBMP = new Bitmap(_drawSize.Width, _drawSize.Height);

            using (var graphics = Graphics.FromImage(_mapBMP))
            {
                for (int y = 0; y < _vcsServer.CurrentMap.Height + 1; y++)
                {
                    for (int x = 0; x < _vcsServer.CurrentMap.Width + 1; x++)
                    {
                        int drawX = x + OffsetX;
                        int drawY = y + OffsetY;
                        if (drawX > _drawSize.Width || drawY > _drawSize.Height) continue;
                        if (x != _vcsServer.CurrentMap.Width && y != _vcsServer.CurrentMap.Height)
                        {
                            var currentNode = _vcsServer.CurrentMap.AllNodes[y, x];
                            Brush brush = new SolidBrush(NodeTypeColorDict[currentNode.Type]);
                            // Draw node
                            if (currentNode.Type != MapNode.Types.None)
                            {
                                graphics.FillRectangle(brush, drawX * _scaledCellSize, drawY * _scaledCellSize, _scaledCellSize, _scaledCellSize);
                            }
                            if (currentNode.DisallowTurningOnNode)
                            {
                                // Draw X on disallow truning node
                                graphics.DrawLine(new Pen(Color.Red, 1), drawX * _scaledCellSize, drawY * _scaledCellSize, (drawX + 1) * _scaledCellSize, (drawY + 1) * _scaledCellSize);
                                graphics.DrawLine(new Pen(Color.Red, 1), (drawX + 1) * _scaledCellSize, drawY * _scaledCellSize, drawX * _scaledCellSize, (drawY + 1) * _scaledCellSize);
                            }
                        }
                        // Draw gird lines
                        graphics.DrawLine(new Pen(Color.White, GridWidth), OffsetX * _scaledCellSize, drawY * _scaledCellSize, (_vcsServer.CurrentMap.Width + OffsetX) * _scaledCellSize, drawY * _scaledCellSize);
                        graphics.DrawLine(new Pen(Color.White, GridWidth), drawX * _scaledCellSize, OffsetY * _scaledCellSize, drawX * _scaledCellSize, (_vcsServer.CurrentMap.Height + OffsetY) * _scaledCellSize);
                    }
                }
            }
        }

        public void DrawSelectedNode(MapNode selectedNode)
        {
            if (selectedNode == null) return;
            using (var graphics = Graphics.FromImage(_mapBMP))
            {
                graphics.DrawRectangle(new Pen(Color.Red, GridWidth), (selectedNode.Location.X + OffsetX) * _scaledCellSize, (selectedNode.Location.Y + OffsetY) * _scaledCellSize, _scaledCellSize, _scaledCellSize);
            }
        }

        public void DrawSelectedEdge(MapNode selectedEdgeNode1, MapNode selectedEdgeNode2)
        {
            if (selectedEdgeNode1 != null && selectedEdgeNode2 != null && selectedEdgeNode1.IsNeighbourNode(selectedEdgeNode2))
            {
                using (var graphics = Graphics.FromImage(_mapBMP))
                {
                    var n1X = selectedEdgeNode1.Location.X;
                    var n1Y = selectedEdgeNode1.Location.Y;
                    var n2X = selectedEdgeNode2.Location.X;
                    var n2Y = selectedEdgeNode2.Location.Y;
                    if (n1X + n1Y < n2X + n2Y)
                    {
                        graphics.DrawRectangle(new Pen(Color.Blue, GridWidth * 2), (n1X + OffsetX) * _scaledCellSize, (n1Y + OffsetY) * _scaledCellSize, (n2X - n1X + 1) * _scaledCellSize, (n2Y - n1Y + 1) * _scaledCellSize);
                    }
                    else
                    {
                        graphics.DrawRectangle(new Pen(Color.Blue, GridWidth * 2), (n2X + OffsetX) * _scaledCellSize, (n2Y + OffsetY) * _scaledCellSize, (n1X - n2X + 1) * _scaledCellSize, (n1Y - n2Y + 1) * _scaledCellSize);
                    }
                }
            }
        }

        public void DrawEdgeConstraints(int selectedLayerIndex = 0)
        {
            using (var graphics = Graphics.FromImage(_mapBMP))
            {
                Pen pen = new Pen(Color.Red, GridWidth * 2);
                for (int y = 0; y < 2 * _vcsServer.CurrentMap.Height - 1; y++)
                {
                    for (int x = 0; x < _vcsServer.CurrentMap.Width; x++)
                    {
                        int drawX = x + OffsetX;
                        int drawY = y + OffsetY * 2;
                        if (drawX > _drawSize.Width || drawY > _drawSize.Height) continue;
                        var edgeConstraints = _vcsServer.CurrentMap.ConstraintLayers[selectedLayerIndex].EdgeConstraints[y, x];
                        var edgePermission = (~((byte)edgeConstraints.PassingRestriction)) & 0x03;
                        if (edgePermission == 0) continue;
                        pen.StartCap = LineCap.NoAnchor;
                        pen.EndCap = LineCap.NoAnchor;
                        if ((edgePermission & 0x01) > 0) pen.EndCap = LineCap.ArrowAnchor;
                        if ((edgePermission & 0x02) > 0) pen.StartCap = LineCap.ArrowAnchor;
                        if (y % 2 == 0)
                        {
                            // Draw horizaontal arrows
                            if (x < _vcsServer.CurrentMap.Width - 1)
                            {
                                graphics.DrawLine(pen, (drawX + 0.5f) * _scaledCellSize, (drawY / 2 + 0.5f) * _scaledCellSize, (drawX + 1.5f) * _scaledCellSize, (drawY / 2 + 0.5f) * _scaledCellSize);
                            }
                        }
                        else
                        {
                            // Draw vertical arrows
                            if (y < 2 * _vcsServer.CurrentMap.Height - 2)
                            {
                                graphics.DrawLine(pen, (drawX + 0.5f) * _scaledCellSize, (drawY / 2 + 0.5f) * _scaledCellSize, (drawX + 0.5f) * _scaledCellSize, (drawY / 2 + 1.5f) * _scaledCellSize);
                            }
                        }
                    }
                }
            }
        }

        public void DrawSinglePath(List<MapNode> path)
        {
            if (path == null) return;
            if (path.Count == 0) return;
            using (var graphics = Graphics.FromImage(_mapBMP))
            {
                Pen pen = new Pen(Color.Green, GridWidth * 1.5f);
                for (int i = 0; i < path.Count; i++)
                {
                    Color interpolatedColor = Color.FromArgb(100, i * 255 / path.Count, 255 - i * 255 / path.Count, 0);
                    pen.Color = interpolatedColor;
                    graphics.DrawRectangle(pen, (path[i].Location.X + OffsetX) * _scaledCellSize, (path[i].Location.Y + OffsetY) * _scaledCellSize, _scaledCellSize, _scaledCellSize);
                }
            }
        }

        public void DrawAGVs()
        {
            if (_vcsServer.AGVHandler.AGVList == null) return;
            if (_vcsServer.AGVHandler.AGVList.Count == 0) return;
            using (var graphics = Graphics.FromImage(_mapBMP))
            {
                foreach (var agv in _vcsServer.AGVHandler.AGVList)
                {
                    Image agvImage = Properties.Resources.AGV;
                    switch (agv.Heading)
                    {
                        case SimulatedAGV.AGVHeading.Up:
                            agvImage.RotateFlip(RotateFlipType.Rotate270FlipNone);
                            break;
                        case SimulatedAGV.AGVHeading.Left:
                            agvImage.RotateFlip(RotateFlipType.Rotate180FlipNone);
                            break;
                        case SimulatedAGV.AGVHeading.Down:
                            agvImage.RotateFlip(RotateFlipType.Rotate90FlipNone);
                            break;
                    }
                    graphics.DrawImage(agvImage, (agv.CurrentNode.Location.X + OffsetX) * _scaledCellSize, (agv.CurrentNode.Location.Y + OffsetY) * _scaledCellSize, _scaledCellSize, _scaledCellSize);
                    graphics.DrawString(agv.ID.ToString(), new Font("Arial", 16), new SolidBrush(Color.DeepSkyBlue), (agv.CurrentNode.Location.X + OffsetX ) * _scaledCellSize + 16, (agv.CurrentNode.Location.Y + OffsetY) * _scaledCellSize + 16);
                }
            }
        }

        public void DrawAllAGVPath()
        {
            if (_vcsServer.AGVHandler.AGVList == null) return;
            if (_vcsServer.AGVHandler.AGVList.Count == 0) return;
            using (var graphics = Graphics.FromImage(_mapBMP))
            {
                foreach (var agv in _vcsServer.AGVHandler.AGVList)
                {
                    if(agv is SimulatedAGV)
                    {
                        Pen pen = new Pen(Color.Green, GridWidth * 1.5f);                        
                        var path = (agv as SimulatedAGV).AssignedPath?.ToList();
                        if (path == null) continue;
                        if (path.Count == 0) continue;
                        pen.Color = Color.FromArgb(100, 0, 255, 0);
                        graphics.DrawRectangle(pen, (agv.CurrentNode.Location.X + OffsetX) * _scaledCellSize, (agv.CurrentNode.Location.Y + OffsetY) * _scaledCellSize, _scaledCellSize, _scaledCellSize);
                        for (int i = 0; i < path.Count; i++)
                        {
                            Color interpolatedColor = Color.FromArgb(100, (i + 1) * 255 / path.Count, 255 - (i + 1) * 255 / path.Count, 0);
                            pen.Color = interpolatedColor;
                            graphics.DrawRectangle(pen, (path[i].Location.X + OffsetX) * _scaledCellSize, (path[i].Location.Y + OffsetY) * _scaledCellSize, _scaledCellSize, _scaledCellSize);
                        }
                    }                    
                }
            }
        }

        public void DrawRacks()
        {
            using (var graphics = Graphics.FromImage(_mapBMP))
            {
                foreach (var rack in _vcsServer.RackList)
                {
                    Image rackImage = null;
                    switch (rack.Heading)
                    {
                        case Rack.RackHeading.Up:
                            rackImage = Properties.Resources.Rack_Up;
                            break;
                        case Rack.RackHeading.Left:
                            rackImage = Properties.Resources.Rack_Left;
                            break;
                        case Rack.RackHeading.Right:
                            rackImage = Properties.Resources.Rack_Right;
                            break;
                        case Rack.RackHeading.Down:
                            rackImage = Properties.Resources.Rack_Down;
                            break;
                    }
                    var agvOnNode = _vcsServer.AGVHandler.AGVList?.Find(agv => agv.CurrentNode == rack.CurrentNode);
                    if (agvOnNode != null)
                    {
                        graphics.DrawImage(rackImage, (rack.CurrentNode.Location.X + OffsetX) * _scaledCellSize, (rack.CurrentNode.Location.Y + OffsetY) * _scaledCellSize, _scaledCellSize / 2, _scaledCellSize / 2);
                        if (agvOnNode.BoundRack == rack)
                        {
                            graphics.DrawImage(Properties.Resources.Link, (rack.CurrentNode.Location.X + OffsetX) * _scaledCellSize, (rack.CurrentNode.Location.Y + OffsetY) * _scaledCellSize, _scaledCellSize, _scaledCellSize);
                        }
                    }
                    else
                    {
                        graphics.DrawImage(rackImage, (rack.CurrentNode.Location.X + OffsetX) * _scaledCellSize, (rack.CurrentNode.Location.Y + OffsetY) * _scaledCellSize, _scaledCellSize, _scaledCellSize);
                    }
                }
            }
        }

        public void DrawOccupancy()
        {
            using (var graphics = Graphics.FromImage(_mapBMP))
            {
                for (int y = 0; y < _vcsServer.CurrentMap.Height; y++)
                {
                    for (int x = 0; x < _vcsServer.CurrentMap.Width; x++)
                    {
                        graphics.DrawString(_vcsServer.OccupancyGrid[y, x].ToString("D1"), new Font("Arial", 16), new SolidBrush(Color.Black), (x + OffsetX) * _scaledCellSize, (y + OffsetY) * _scaledCellSize);
                    }
                }
            }
        }
        public MapNode GetNodeByPosition(int x, int y)
        {
            int nodeX = x / _scaledCellSize - OffsetX;
            int nodeY = y / _scaledCellSize - OffsetY;
            if (nodeX < 0 || nodeY < 0) return null;
            if (nodeX >= _vcsServer.CurrentMap.Width || nodeY >= _vcsServer.CurrentMap.Height) return null;
            return _vcsServer.CurrentMap.AllNodes[nodeY, nodeX];
        }
    }
}
