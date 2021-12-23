using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using A_Star_Demo.AGVs;
using A_Star_Demo.Maps;
using A_Star_Demo.Models;

namespace A_Star_Demo.Tasks
{
    public class AGVTaskHandler
    {
        public AGV AGV { get; }
        public AGVTask CurrentTask { get; private set; }
        public Queue<AGVTask> TaskQueue { get; }
        public List<AGVTask> FinishedTaskList { get; }
        public AGVTaskHandler(AGV agv)
        {
            this.AGV = agv;
            this.TaskQueue = new Queue<AGVTask>();
            this.FinishedTaskList = new List<AGVTask>();
            var t = new Thread(TaskHandleThread);
            t.IsBackground = true;
            t.Start();
        }

        public void NewAGVMoveTask(MapNode destination)
        {
            TaskQueue.Enqueue(new AGVMoveTask(this, destination));
        }
        public void NewRackPickUpTask(Rack targetRack)
        {
            TaskQueue.Enqueue(new RackPickUpTask(this, targetRack));
        }
        public void NewRackDropOffTask()
        {
            TaskQueue.Enqueue(new RackDropOffTask(this));
        }

        public void NewRackRotateTask(Rack.RackHeading targetHeading)
        {
            TaskQueue.Enqueue(new RackRotateTask(this, targetHeading));
        }

        public void TaskHandleThread()
        {
            while (true)
            {
                if (this.CurrentTask == null)
                {
                    if (this.TaskQueue.Count > 0 && this.AGV.State == AGV.AGVStates.Idle) this.CurrentTask = this.TaskQueue.Dequeue();
                }
                else
                {
                    if (this.CurrentTask.Finished)
                    {
                        this.FinishedTaskList.Add(this.CurrentTask);
                        this.CurrentTask = null;
                    }
                    else
                    {
                        this.CurrentTask.Execute();
                    }
                }                
                Thread.Sleep(10);
            }
        }        
    }
}
