using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using A_Star_Demo.Maps;
using A_Star_Demo.Models;

namespace A_Star_Demo.AGVs
{
    public class SimulatedAGV
    {
        #region Enums
        [Flags]
        public enum AGVStatus
        {           
            Idle = 0,                       //等待命令中
            Moving,                    //工作中
            Rotating,
            Docking,                    //頂料中            
            UnDocking,                  //頂料中
            Charging,                   //充電中            
            ChargeFinished,

            //以下為異常狀態, 從100開始, Server顯示紅字警告
            Disconnected = 100,       //斷線(由Server來判斷)         
        }

        public enum AGVHeading : int
        {
            Up = 90,
            Down = -90,
            Left = -180,
            Right = 0
        }
        #endregion
        public int ID { get; }
        public string Name { get; private set; }
        public MapNode Node { get; private set; }

        public AGVStatus Status { get; private set; }

        public AGVHeading Heading { get; private set; }

        public SimulatedAGV(int id, string name, MapNode node)
        {
            this.ID = id;
            this.Name = name;
            this.Node = node;
            this.Status = AGVStatus.Idle;
            this.Heading = AGVHeading.Right;
        }        

    }
}
