using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VCS.Models
{
    public class Location
    {
        public int X { get; private set; }
        public int Y { get; private set; }
        public Location(int x, int y)
        {
            X = x;
            Y = y;
        }

        public Location(Location location)
        {
            X = location.X;
            Y = location.Y;
        }

        public override string ToString()
        {
            return $"({X}, {Y})";
        }
    }
}
