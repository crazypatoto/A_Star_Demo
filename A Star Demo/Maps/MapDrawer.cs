using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Diagnostics;
using A_Star_Demo.Maps;
using A_Star_Demo.Models;

namespace A_Star_Demo.Dialogs
{
    public class MapDrawer
    {
        private Map _map;
        private Bitmap _mapBMP;
        private Size _drawSize;
        private int _cellSize;
        public int GridWidth { get; set; } = 3;
        public static Dictionary<MapNode.Types, Color> NodeTypeColorDict = new Dictionary<MapNode.Types, Color> {
            {MapNode.Types.None, Color.White },
            {MapNode.Types.Storage, Color.Orange },
            {MapNode.Types.ChargingStation, Color.Yellow },
            {MapNode.Types.WorkStation, Color.LightBlue},
            {MapNode.Types.Wall, Color.DarkGray },
        };

        public MapDrawer(ref Map map, int drawWidth, int drawHeight)
        {
            _map = map;
            _drawSize = new Size(drawWidth, drawHeight);
            _cellSize = Math.Min(_drawSize.Width / _map.Width, _drawSize.Height / _map.Height);
        }

        public Bitmap GetMapPicture(MapNode selectedNode, MapNode selectedEdgeNode1 = null, MapNode selectedEdgeNode2 = null, bool drawEdgeConstraints = false, int selectedLayerIndex = 0, List<MapNode> path = null)
        {
            _mapBMP?.Dispose();
            _mapBMP = new Bitmap(_drawSize.Width, _drawSize.Height);

            using (var graphics = Graphics.FromImage(_mapBMP))
            {
                for (int y = 0; y < _map.Height + 1; y++)
                {
                    for (int x = 0; x < _map.Width + 1; x++)
                    {
                        if (x != _map.Width && y != _map.Height)
                        {
                            var currentNode = _map.AllNodes[y, x];
                            Brush brush = new SolidBrush(NodeTypeColorDict[currentNode.Type]);
                            graphics.FillRectangle(brush, x * _cellSize, y * _cellSize, _cellSize, _cellSize);
                            //graphics.DrawEllipse(new Pen(Color.Red, 1), x * _cellSize + 1, y * _cellSize + 1, _cellSize - 2, _cellSize - 2);
                            if (currentNode.DisallowTurningOnNode)
                            {
                                graphics.DrawLine(new Pen(Color.Red, 1), x * _cellSize, y * _cellSize, (x + 1) * _cellSize, (y + 1) * _cellSize);
                                graphics.DrawLine(new Pen(Color.Red, 1), (x + 1) * _cellSize, y * _cellSize, x * _cellSize, (y + 1) * _cellSize);
                            }                            
                        }
                        graphics.DrawLine(new Pen(Color.Black, GridWidth), 0, y * _cellSize, _map.Width * _cellSize, y * _cellSize);
                        graphics.DrawLine(new Pen(Color.Black, GridWidth), x * _cellSize, 0, x * _cellSize, _map.Height * _cellSize);

                    }
                }
                if (selectedNode != null)
                {
                    graphics.DrawRectangle(new Pen(Color.Red, GridWidth), selectedNode.Location.X * _cellSize, selectedNode.Location.Y * _cellSize, _cellSize, _cellSize);
                }
                if (selectedEdgeNode1 != null && selectedEdgeNode2 != null && selectedEdgeNode1.IsNeighbourNode(selectedEdgeNode2))
                {

                    var n1X = selectedEdgeNode1.Location.X;
                    var n1Y = selectedEdgeNode1.Location.Y;
                    var n2X = selectedEdgeNode2.Location.X;
                    var n2Y = selectedEdgeNode2.Location.Y;
                    if (n1X + n1Y < n2X + n2Y)
                    {
                        graphics.DrawRectangle(new Pen(Color.Blue, GridWidth * 2), n1X * _cellSize, n1Y * _cellSize, (n2X - n1X + 1) * _cellSize, (n2Y - n1Y + 1) * _cellSize);
                    }
                    else
                    {
                        graphics.DrawRectangle(new Pen(Color.Blue, GridWidth * 2), n2X * _cellSize, n2Y * _cellSize, (n1X - n2X + 1) * _cellSize, (n1Y - n2Y + 1) * _cellSize);
                    }
                }
                if (drawEdgeConstraints)
                {
                    Pen pen = new Pen(Color.Red, GridWidth * 2);
                    for (int y = 0; y < 2 * _map.Height - 1; y++)
                    {
                        for (int x = 0; x < _map.Width; x++)
                        {
                            var edgeConstraints = _map.ConstraintLayers[selectedLayerIndex].EdgeConstraints[y, x];
                            var edgePermission = (~((byte)edgeConstraints.PassingRestriction)) & 0x03;
                            if (edgePermission == 0) continue;
                            pen.StartCap = LineCap.NoAnchor;
                            pen.EndCap = LineCap.NoAnchor;
                            if ((edgePermission & 0x01) > 0) pen.EndCap = LineCap.ArrowAnchor;
                            if ((edgePermission & 0x02) > 0) pen.StartCap = LineCap.ArrowAnchor;
                            if (y % 2 == 0)     // Draw horizaontal arrows
                            {
                                if (x < _map.Width - 1)
                                {
                                    graphics.DrawLine(pen, (x + 0.5f) * _cellSize, (y / 2 + 0.5f) * _cellSize, (x + 1.5f) * _cellSize, (y / 2 + 0.5f) * _cellSize);
                                }
                            }
                            else                // Draw vertical arrows
                            {
                                if (y < 2 * _map.Height - 2)
                                {
                                    graphics.DrawLine(pen, (x + 0.5f) * _cellSize, (y / 2 + 0.5f) * _cellSize, (x + 0.5f) * _cellSize, (y / 2 + 1.5f) * _cellSize);
                                }
                            }
                        }
                    }
                }
                if (path != null)
                {
                    Pen pen = new Pen(Color.Green, GridWidth * 2);
                    for (int i = 0; i < path.Count; i++)
                    {
                        Color interpolatedColor = Color.FromArgb(i * 255 / path.Count, 255 - i * 255 / path.Count, 0);
                        pen.Color = interpolatedColor;
                        graphics.DrawRectangle(pen, path[i].Location.X * _cellSize, path[i].Location.Y * _cellSize, _cellSize, _cellSize);
                    }
                }
            }
            return _mapBMP;
        }


        public MapNode GetNodeByPosition(int x, int y)
        {
            int nodeX = x / _cellSize;
            int nodeY = y / _cellSize;
            if (nodeX < 0 || nodeY < 0) return null;
            if (nodeX >= _map.Width || nodeY >= _map.Height) return null;
            return _map.AllNodes[nodeY, nodeX];
        }
    }
}
