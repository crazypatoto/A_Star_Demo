using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using A_Star_Demo.Maps;

namespace A_Star_Demo.AGVs
{
    public class AGVHandler
    {
        public List<SimulatedAGV> AGVList { get; private set; }
        public AGVHandler()
        {
            AGVList = new List<SimulatedAGV>();
        }

        public SimulatedAGV AddSimulatedAGV(MapNode node, string name = null)
        {
            if (node == null) return null;
            int newID = 0;
            if (AGVList.Count != 0)
            {
                newID = AGVList.Last().ID + 1;
            }
            AGVList.Add(new SimulatedAGV(newID, name ?? $"AGV{newID.ToString("D3")}", node));
            return AGVList.Last();
        }
    }
}
