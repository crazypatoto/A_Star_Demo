using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using VCS.AGVs;
using VCS.Maps;
using VCS.Models;

namespace VCS.Tasks
{
    public class AGVTaskHandler
    {
        public AGV AGV { get; }
        public AGVTask CurrentTask { get; private set; }
        public Queue<AGVTask> TaskQueue { get; }
        public List<AGVTask> FinishedTaskList { get; }
        private readonly CancellationTokenSource _cts;
        public AGVTaskHandler(AGV agv)
        {
            this.AGV = agv;
            this.TaskQueue = new Queue<AGVTask>();
            this.FinishedTaskList = new List<AGVTask>();
            _cts = new CancellationTokenSource();
            Task.Run(TaskHandling);
            //var t = new Thread(TaskHandling);
            //t.IsBackground = true;
            //t.Start();
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
        public void NewAGVWaitTask()
        {
            TaskQueue.Enqueue(new AGVWaitTask(this));
        }

        public void Abort()
        {
            _cts.Cancel();
        }

        private async void TaskHandling()
        {
            while (!_cts.IsCancellationRequested)
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
                        if(this.FinishedTaskList.Count > 10)
                        {
                            this.FinishedTaskList.RemoveAt(0);
                        }
                        this.CurrentTask = null;
                    }
                    else
                    {
                        this.CurrentTask.Execute();
                    }
                }
                await Task.Delay(10);
            }
        }
    }
}
