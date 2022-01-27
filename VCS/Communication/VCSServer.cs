using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Collections.Generic;
using VCS.Maps;
using VCS.Models;
using VCS.Missions;

namespace VCS.Communication
{
    public class VCSServer
    {
        private readonly int _port = 2312;
        private Socket _serverSocket;
        private Socket _clientSocket;
        private VCS _vcs;
        private long _lastCommunicateTime;
        public VCSServer(VCS vcs)
        {
            _vcs = vcs;
            _serverSocket = new Socket(SocketType.Stream, ProtocolType.Tcp);
            _serverSocket.Bind(new IPEndPoint(IPAddress.Any, _port));
            _serverSocket.Listen(1);
            Thread t = new Thread(ClientHandling);
            t.IsBackground = true;
            t.Start();
        }

        private void ClientHandling()
        {
            var buffer = new byte[1024];
            var count = 0;

            while (_vcs.IsAlive)
            {
                if (_clientSocket == null)
                {
                    _clientSocket = _serverSocket.Accept();
                    _lastCommunicateTime = DateTimeOffset.Now.ToUnixTimeSeconds();
                }
                else
                {
                    if (_clientSocket.Connected)
                    {
                        try
                        {
                            var now = DateTimeOffset.Now.ToUnixTimeSeconds();
                            var read = _clientSocket.Receive(buffer, count, buffer.Length - count, SocketFlags.None);
                            count += read;

                            if (count >= 1024)
                            {
                                count = 0;
                            }

                            if (count > 2 && buffer[count - 2] == '\r' && buffer[count - 1] == '\n')
                            {
                                buffer[count] = 0;
                                HandleRequest(Encoding.UTF8.GetString(buffer, 0, count));
                                _lastCommunicateTime = now;
                                count = 0;
                            }

                            if (now - _lastCommunicateTime > 3)
                            {
                                _clientSocket.Close();
                            }
                        }
                        catch (Exception ex)
                        {
                            Debug.WriteLine(ex.Message.ToString());
                            _clientSocket.Dispose();
                            _clientSocket = null;
                            _serverSocket.Listen(1);
                        }
                    }
                    else
                    {
                        _clientSocket.Dispose();
                        _clientSocket = null;
                        _serverSocket.Listen(1);
                    }
                }
            }
        }

        private void HandleRequest(string requestString)
        {
            var request = new VCSRequest(requestString);
            if (!request.IsValid) return;
            switch (request.Command)
            {
                case Command.CheckConnection:
                    CheckConnectionRequest(request);
                    break;
                case Command.GetMapData:
                    GetMapDataRequest(request);
                    break;
                case Command.GetRackInfo:
                    GetRackInfoRequest(request);
                    break;
                case Command.AssignNewWorkOrder:
                    AssignNewWorkOrderRequest(request);
                    break;
                case Command.GetWorkOrderState:
                    GetWorkOrderStateRequest(request);
                    break;
                case Command.CancelWorkOrder:
                    CancelWorkOrderRequest(request);
                    break;
                case Command.GetCurrentWorkOrderUUID:
                    GetCurrentWorkOrderUUIDRequest(request);
                    break;
                default:
                    break;
            }
        }
        private void SendResponse(Command command, string commandData)
        {
            var timestamp = DateTimeOffset.Now.ToUnixTimeSeconds().ToString();
            var response = timestamp + ";" + command + ";" + commandData + ";" + "\r\n";
            _clientSocket.Send(Encoding.UTF8.GetBytes(response));
        }
        private void CheckConnectionRequest(VCSRequest request)
        {
            SendResponse(Command.CheckConnection, "OK");
        }

        private void GetMapDataRequest(VCSRequest request)
        {
            var tempMapFile = System.IO.Path.GetTempFileName();
            _vcs.CurrentMap.SaveToFile(tempMapFile);
            var mapBytes = File.ReadAllBytes(tempMapFile);
            var commandData = Convert.ToBase64String(mapBytes);
            SendResponse(Command.GetMapData, commandData);
            File.Delete(tempMapFile);
        }

        private void GetRackInfoRequest(VCSRequest request)
        {
            var commandData = _vcs.RackList.Count.ToString() + ",";
            for (int i = 0; i < _vcs.RackList.Count; i++)
            {
                var rack = _vcs.RackList[i];
                commandData += $"{rack.Name},{rack.HomeNode.Name},{rack.CurrentNode.Name},{rack.Heading}";
                if (i != _vcs.RackList.Count - 1)
                {
                    commandData += ",";
                }
            }
            SendResponse(Command.GetRackInfo, commandData);
        }

        private void AssignNewWorkOrderRequest(VCSRequest request)
        {            
            var dataSegments = request.Data.Split(',');
            var uuid = dataSegments[0];
            var missionCount = int.Parse(dataSegments[1]);
            var missionList = new List<Mission>();
            for (int i = 0; i < missionCount; i++)
            {
                var offset = i * 3;                
                var targetRack = _vcs.RackList.Find(rack => rack.Name == dataSegments[offset + 2]);         
                var destX = int.Parse(dataSegments[offset + 3].Split('-')[1]);
                var destY = int.Parse(dataSegments[offset + 3].Split('-')[2]);
                var destNode = _vcs.CurrentMap.AllNodes[destY, destX];
                var pickUpFace = dataSegments[offset + 4];
                var neighborNodes = _vcs.CurrentMap.GetNeighborNodes(destNode);
                var pickUpNode = neighborNodes.Find(node => node.Type == MapNode.Types.WorkStationPickUp);
                var pickUpDirection = Math.Atan2(pickUpNode.Location.Y - destNode.Location.Y, pickUpNode.Location.X - destNode.Location.X) / Math.PI * 180;
                int pickUpFaceAngle = 0;
                switch (pickUpFace)
                {
                    case "North":
                        pickUpFaceAngle = -90;
                        break;
                    case "East":
                        pickUpFaceAngle = 180;
                        break;
                    case "West":
                        pickUpFaceAngle = 0;
                        break;
                    case "South":
                        pickUpFaceAngle = 90;
                        break;
                }
                var targetHeading = pickUpDirection - pickUpFaceAngle;
                var newMission = new Mission(targetRack, destNode, (Rack.RackHeading)targetHeading);
                missionList.Add(newMission);                
            }
            _vcs.MissionHandler.HandleNewWorkOrder(uuid, missionList);
            var commandData = uuid + "," + "Accepted";
            SendResponse(Command.AssignNewWorkOrder, commandData);
        }

        private void GetWorkOrderStateRequest(VCSRequest request)
        {
            var uuid = request.Data;
            if(_vcs.MissionHandler.CurrentWorkOrderUUID == uuid)
            {
                var commandData = uuid + "," + _vcs.MissionHandler.State.ToString();
                SendResponse(Command.GetWorkOrderState, commandData);
            }
            else
            {
                SendResponse(Command.GetWorkOrderState, "Failed,Unknown Work Order UUID");
            }
        }

        private void CancelWorkOrderRequest(VCSRequest request)
        {

        }

        private void GetCurrentWorkOrderUUIDRequest(VCSRequest request)
        {
            SendResponse(Command.GetWorkOrderState, _vcs.MissionHandler.CurrentWorkOrderUUID);
        }
    }
}
