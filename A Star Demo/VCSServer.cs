using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using A_Star_Demo.AGVs;
using A_Star_Demo.Maps;
using A_Star_Demo.Models;
using A_Star_Demo.PathPlanning;
using A_Star_Demo.Tasks;

namespace A_Star_Demo
{
    public class VCSServer
    {
        public Map CurrentMap { get; }
        public AGVTaskHandler TaskHandler { get; }
        public AGVHandler AGVHandler { get; }
        public AStarPlanner PathPlanner { get; }
        public List<Rack> RackList;

        #region Constructors
        public VCSServer(Map map)
        {
            CurrentMap = map;
            TaskHandler = new AGVTaskHandler(this);
            AGVHandler = new AGVHandler(this);
            PathPlanner = new AStarPlanner(this);
            RackList = new List<Rack>();
        }
        #endregion       
    }
}
