using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;
using VCS.Maps;
using VCS.Models;
using VCS.AGVs;


namespace WMS
{
    public class MapDrawerSlim
    {
        private readonly int _minCellSize = 20;
        private Map _currentMap;
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
                _cellSize = Math.Min(DrawSize.Width / _currentMap.Width, DrawSize.Height / _currentMap.Height);
                if (_cellSize < _minCellSize) _cellSize = _minCellSize;
                //_scale = (float)_scaledCellSize / _cellSize;
                this.Scale = 1;
            }
        }

        public static Dictionary<MapNode.Types, Color> NodeTypeColorDict = new Dictionary<MapNode.Types, Color> {
            {MapNode.Types.None, Color.White },
            {MapNode.Types.Storage, Color.Orange },
            {MapNode.Types.ChargingStation, Color.Yellow },
            {MapNode.Types.WorkStationArea, Color.LightBlue},
            {MapNode.Types.WorkStation, Color.DeepSkyBlue},
            {MapNode.Types.Wall, Color.DarkGray },
        };

        public MapDrawerSlim(Map map, Size drawSize)
        {
            _currentMap = map;
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
                for (int y = 0; y < _currentMap.Height + 1; y++)
                {
                    for (int x = 0; x < _currentMap.Width + 1; x++)
                    {
                        int drawX = x + OffsetX;
                        int drawY = y + OffsetY;
                        if (drawX > _drawSize.Width || drawY > _drawSize.Height) continue;
                        if (x != _currentMap.Width && y != _currentMap.Height)
                        {
                            var currentNode = _currentMap.AllNodes[y, x];
                            Brush brush = new SolidBrush(NodeTypeColorDict[currentNode.Type]);
                            // Draw node
                            if (currentNode.Type != MapNode.Types.None)
                            {
                                graphics.FillRectangle(brush, drawX * _scaledCellSize, drawY * _scaledCellSize, _scaledCellSize, _scaledCellSize);
                            }                            
                        }
                        // Draw gird lines
                        graphics.DrawLine(new Pen(Color.White, GridWidth), OffsetX * _scaledCellSize, drawY * _scaledCellSize, (_currentMap.Width + OffsetX) * _scaledCellSize, drawY * _scaledCellSize);
                        graphics.DrawLine(new Pen(Color.White, GridWidth), drawX * _scaledCellSize, OffsetY * _scaledCellSize, drawX * _scaledCellSize, (_currentMap.Height + OffsetY) * _scaledCellSize);
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
               
       
        public void DrawRacks(List<Rack> rackList)
        {
            using (var graphics = Graphics.FromImage(_mapBMP))
            {
                foreach (var rack in rackList)
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
                    graphics.DrawImage(rackImage, (rack.CurrentNode.Location.X + OffsetX) * _scaledCellSize, (rack.CurrentNode.Location.Y + OffsetY) * _scaledCellSize, _scaledCellSize, _scaledCellSize);
                    graphics.DrawString(rack.ID.ToString(), new Font("Consolas", (int)(_scaledCellSize*0.4)), new SolidBrush(Color.White), (rack.CurrentNode.Location.X + OffsetX) * _scaledCellSize + _scaledCellSize, (rack.CurrentNode.Location.Y + OffsetY) * _scaledCellSize , new StringFormat() { Alignment = StringAlignment.Far});
                }
            }
        }
      
        public MapNode GetNodeByPosition(int x, int y)
        {
            int nodeX = x / _scaledCellSize - OffsetX;
            int nodeY = y / _scaledCellSize - OffsetY;
            if (nodeX < 0 || nodeY < 0) return null;
            if (nodeX >= _currentMap.Width || nodeY >= _currentMap.Height) return null;
            return _currentMap.AllNodes[nodeY, nodeX];
        }
    }
}

