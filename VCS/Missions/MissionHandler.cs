using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using VCS;
using VCS.Maps;
using VCS.Models;
using VCS.AGVs;
using VCS.Tasks;

namespace VCS.Missions
{
    public class MissionHandler
    {
        public enum WorkOrderState
        {
            Executing,
            Finished,
            Error,
        }

        private VCS _vcs;
        public List<Mission> MissionList { get; private set; }
        public string CurrentWorkOrderUUID { get; private set; }
        public WorkOrderState State { get; private set; }

        public MissionHandler(VCS vcs)
        {
            _vcs = vcs;
            this.MissionList = new List<Mission>();
            Thread t = new Thread(HandleMissions);
            t.IsBackground = true;
            t.Start();
        }

        private void HandleMissions()
        {
            while (_vcs.IsAlive)
            {
                for (int i = 0; i < this.MissionList.Count; i++)
                {
                    var mission = this.MissionList[i];
                    var prevMission = i - 1 >= 0 ? this.MissionList[i - 1] : null;
                    var nextMission = i + 1 < this.MissionList.Count ? this.MissionList[i + 1] : null;

                    switch (mission.State)
                    {
                        case Mission.MissionState.Initialized:
                            if (prevMission?.TargetRack == mission.TargetRack)
                            {
                                if (prevMission.AssignedAGV != null)
                                {
                                    mission.AssignAGV(prevMission.AssignedAGV);
                                    mission.State = Mission.MissionState.PickingUpRack;
                                }
                            }
                            else
                            {
                                var chosenAGV = SelectAGV(mission);
                                if (chosenAGV != null)
                                {
                                    mission.AssignAGV(chosenAGV);
                                    mission.AssignedAGV.TaskHandler.NewAGVMoveTask(mission.TargetRack.CurrentNode); // Move AGV to rack's position
                                    mission.AssignedAGV.TaskHandler.NewRackPickUpTask(mission.TargetRack);  // Pick up target rack
                                    mission.State = Mission.MissionState.PickingUpRack;
                                }
                            }
                            break;
                        case Mission.MissionState.PickingUpRack:
                            if (mission.AssignedAGV.BoundRack == mission.TargetRack && mission.AssignedAGV.State == AGV.AGVStates.Idle)
                            {
                                if (prevMission?.TargetRack == mission.TargetRack)
                                {
                                    mission.State = Mission.MissionState.GoingToDestination;
                                }
                                else
                                {
                                    mission.AssignedAGV.TaskHandler.NewAGVMoveTask(mission.Destination);    // Move AGV to destination
                                    mission.State = Mission.MissionState.GoingToDestination;
                                }
                            }
                            break;
                        case Mission.MissionState.GoingToDestination:
                            if (mission.AssignedAGV.CurrentNode == mission.Destination && mission.AssignedAGV.State == AGV.AGVStates.Idle)
                            {
                                mission.AssignedAGV.TaskHandler.NewRackRotateTask(mission.TargetRackHeading);
                                mission.AssignedAGV.TaskHandler.NewAGVWaitTask();
                                mission.State = Mission.MissionState.RotatingRack;
                            }
                            break;
                        case Mission.MissionState.RotatingRack:
                            var lastTask = mission.AssignedAGV.TaskHandler.FinishedTaskList.LastOrDefault();
                            if (lastTask != null && lastTask.GetType() == typeof(RackRotateTask))
                            {
                                if (mission.AssignedAGV.BoundRack.Heading == mission.TargetRackHeading && (lastTask as RackRotateTask).TargetHeading == mission.TargetRackHeading)
                                {
                                    mission.State = Mission.MissionState.WaitingUserResume;
                                }
                            }
                            break;
                        case Mission.MissionState.WaitingUserResume:
                            if (nextMission?.TargetRack == mission.TargetRack)
                            {
                                if (nextMission.State == Mission.MissionState.TakingRackHome)
                                {
                                    mission.State = nextMission.State;
                                }
                            }
                            else
                            {
                                var lastFinishedTask = mission.AssignedAGV.TaskHandler.FinishedTaskList.LastOrDefault();
                                if (lastFinishedTask != null)
                                {
                                    if (lastFinishedTask.GetType() == typeof(AGVWaitTask))
                                    {
                                        mission.AssignedAGV.TaskHandler.NewAGVMoveTask(mission.TargetRack.HomeNode);
                                        mission.AssignedAGV.TaskHandler.NewRackDropOffTask();
                                        mission.State = Mission.MissionState.TakingRackHome;
                                    }
                                }
                            }
                            break;
                        case Mission.MissionState.TakingRackHome:
                            if (mission.AssignedAGV.BoundRack == null && mission.AssignedAGV.State == AGV.AGVStates.Idle)
                            {
                                mission.State = Mission.MissionState.Done;
                            }
                            break;
                        case Mission.MissionState.Done:
                            break;
                        case Mission.MissionState.Error:
                            break;
                        default:
                            break;
                    }
                }
                this.MissionList.RemoveAll(mission => mission.State == Mission.MissionState.Done);

                bool workOrderDoneFlag = true;
                foreach (var mission in this.MissionList)
                {
                    if (mission.State < Mission.MissionState.TakingRackHome)
                    {
                        workOrderDoneFlag = false;
                        break;
                    }
                }
                if (workOrderDoneFlag) this.State = WorkOrderState.Finished;
                Thread.Sleep(10);
            }
        }

        // Mission list must be sorted in order of rack IDs before processing
        public void HandleNewWorkOrder(string UUID, List<Mission> missionList)
        {
            this.CurrentWorkOrderUUID = UUID;
            this.MissionList.AddRange(missionList);
            this.State = WorkOrderState.Executing;
        }

        private AGV SelectAGV(Mission mission)
        {
            // Select AGV base on shortest distance to target rack.        
            var agvPriorityList = _vcs.AGVHandler.AGVList.ToList();
            var busyMissions = this.MissionList.FindAll(mis => mis.State != Mission.MissionState.Done);
            var agvsInMission = busyMissions.Select(mis => mis.AssignedAGV).ToList();

            agvPriorityList = agvPriorityList.Except(agvsInMission).ToList();
            agvPriorityList = agvPriorityList.
                OrderBy(agv =>
                {
                    var path = _vcs.PathPlanner.FindPath(agv.CurrentNode, mission.TargetRack.CurrentNode, false, 0);
                    if (path != null)
                    {
                        return path.Count;
                    }
                    else
                    {
                        return Math.Abs(mission.TargetRack.CurrentNode.Location.X - agv.CurrentNode.Location.X) + Math.Abs(mission.TargetRack.CurrentNode.Location.Y - agv.CurrentNode.Location.Y);
                    }
                }).ToList();
            return agvPriorityList.FirstOrDefault();
        }
    }
}
;