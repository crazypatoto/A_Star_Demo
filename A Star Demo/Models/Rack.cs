﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using A_Star_Demo.Maps;

namespace A_Star_Demo.Models
{
    public class Rack
    {
        #region Enums
        public enum RackHeading : int
        {
            Up = 90,
            Down = -90,
            Left = 180,
            Right = 0
        }
        #endregion
        public int ID { get; }
        public string Name { get; private set; }

        public MapNode HomeNode { get; private set; }

        public MapNode CurrentNode { get; private set; }
        public RackHeading Heading
        {
            get
            {
                return _heading;
            }
            private set
            {
                _heading = value;
                _heading = (RackHeading)((int)_heading % 360);
                if ((int)_heading > 180) _heading -= 180;
                else if ((int)_heading <= -180) _heading += 180;
            }
        }
        private RackHeading _heading;

        public Rack(int id, MapNode node, RackHeading heading, string name = null, MapNode homeNode = null)
        {
            this.ID = id;
            this.Name = name ?? $"Rack{id:D3}";
            this.CurrentNode = node;
            this.HomeNode = homeNode ?? node;
            this.Heading = heading;
        }
    }
}