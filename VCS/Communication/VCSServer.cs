using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;
using System.Diagnostics;


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
                           
                            if(now - _lastCommunicateTime > 3)
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
            Debug.WriteLine(request.Timestamp.ToString());
            Debug.WriteLine(request.Command);
            Debug.WriteLine(request.Data);
            switch (request.Command)
            {
                case Command.CheckConnection:
                    CheckConnectionRequest();
                    break;
                case Command.GetMapData:
                    GetMapDataRequest();
                    break;
                case Command.GetRackInfo:
                    GetRackInfoRequest();
                    break;
                case Command.AssignNewMission:
                    AssignNewMissionRequest();
                    break;
                case Command.GetMissionState:
                    GetMissionStateRequest();
                    break;
                case Command.CancelMission:
                    CancelMissionRequest();
                    break;
                case Command.GetCurrentMission:
                    GetCurrentMissionRequest();
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
        private void CheckConnectionRequest()
        {
            SendResponse(Command.CheckConnection, "OK");
        }

        private void GetMapDataRequest()
        {
            var tempMapFile = System.IO.Path.GetTempFileName();
            _vcs.CurrentMap.SaveToFile(tempMapFile);
            var mapBytes = File.ReadAllBytes(tempMapFile);
            var commandData = Convert.ToBase64String(mapBytes);
            SendResponse(Command.GetMapData, commandData);
            File.Delete(tempMapFile);
        }

        private void GetRackInfoRequest()
        {
            var commandData = _vcs.RackList.Count.ToString() + ",";
            for (int i = 0; i < _vcs.RackList.Count; i++)
            {
                var rack = _vcs.RackList[i];
                commandData += $"{rack.Name},{rack.HomeNode.Name},{rack.CurrentNode.Name},{rack.Heading}";
                if(i != _vcs.RackList.Count - 1)
                {
                    commandData += ",";
                }
            }
            SendResponse(Command.GetRackInfo, commandData);
        }

        private void AssignNewMissionRequest()
        {

        }

        private void GetMissionStateRequest()
        {

        }

        private void CancelMissionRequest()
        {

        }

        private void GetCurrentMissionRequest()
        {

        }
    }
}
