using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

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

        public Bitmap GetMapPicture()
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
                            Brush brush = new SolidBrush(NodeTypeColorDict[_map.AllNodes[y, x].Type]);
                            graphics.FillRectangle(brush, x * _cellSize, y * _cellSize, _cellSize, _cellSize);
                        }
                        graphics.DrawLine(new Pen(Color.Black, GridWidth), 0, y * _cellSize, _map.Width * _cellSize, y * _cellSize);
                        graphics.DrawLine(new Pen(Color.Black, GridWidth), x * _cellSize, 0, x * _cellSize, _map.Height * _cellSize);

                    }
                }
            }
            return _mapBMP;
        }

        public MapNode GetNodeByPosition(int x, int y)
        {
            int nodeX = x / _cellSize;
            int nodeY = y / _cellSize;
            if (nodeX < _map.Width && nodeY < _map.Height)
            {
                return _map.AllNodes[nodeY, nodeX];
            }
            else
            {
                return null;
            }
        }
    }
}
