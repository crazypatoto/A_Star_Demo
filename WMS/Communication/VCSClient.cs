using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net.Sockets;
using System.Net;
using System.Text.RegularExpressions;
using System.Diagnostics;
using VCS.Maps;
using VCS.Models;
using System.IO;

namespace WMS.Communication
{
    public class VCSClient
    {
        private readonly int _port = 2312;
        private readonly int _sendTimeout = 100;
        private readonly int _receiveTimeout = 1000;
        private readonly int _rxBuffeSize = 1024 * 1000; // 1MB
        private Socket _clientSocket;
        private Map _currentMap;
        private List<Rack> _rackList;
        public bool IsConnected { get; private set; }

        public VCSClient()
        {
            this.IsConnected = false;
        }

        public bool Connect(string ip, int port = default)
        {
            _clientSocket = new Socket(SocketType.Stream, ProtocolType.Tcp);
            if (port == default) port = _port;
            try
            {
                _clientSocket.Connect(new IPEndPoint(IPAddress.Parse(ip), port));
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return false;
            }
            if (_clientSocket.Connected)
            {
                if (CheckConnection())
                {
                    this.IsConnected = true;
                    return true;
                }
            }
            return false;
        }

        public void Disconnect()
        {
            if (_clientSocket.Connected)
            {
                _clientSocket.Close();
            }
        }
        private VCSResponse SendRequest(Command command, string data = "")
        {
            var timestamp = DateTimeOffset.Now.ToUnixTimeSeconds().ToString();
            var requestString = timestamp + ";" + command + ";" + data + ";" + "\r\n";
            var request = Encoding.UTF8.GetBytes(requestString);
            _clientSocket.SendTimeout = _sendTimeout;
            _clientSocket.Send(request);

            using (var receiveCts = new CancellationTokenSource(_receiveTimeout))
            {
                var buffer = new byte[_rxBuffeSize]; //1MB
                var count = 0;
                while (!receiveCts.IsCancellationRequested)
                {
                    try
                    {
                        _clientSocket.ReceiveTimeout = _receiveTimeout;
                        var read = _clientSocket.Receive(buffer, count, buffer.Length - count, SocketFlags.None);
                        count += read;

                        if (read == 0)
                        {
                            return VCSResponse.Empty;
                        }

                        if (count >= _rxBuffeSize)
                        {
                            count = 0;
                        }

                        if (count > 2 && buffer[count - 2] == '\r' && buffer[count - 1] == '\n')
                        {
                            var response = new VCSResponse(Encoding.UTF8.GetString(buffer, 0, count));
                            if (response.IsValid)
                            {
                                return response;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(ex.Message);
                    }
                }
            }
            return VCSResponse.Empty;
        }

        public bool CheckConnection()
        {
            var response = SendRequest(Command.CheckConnection);
            if (response.IsValid)
            {
                if (response.Data == "OK")
                {
                    return true;
                }
            }
            return false;
        }

        public Map GetMapData()
        {
            var response = SendRequest(Command.GetMapData);
            if (response.IsValid)
            {
                var tempFile = Path.GetTempFileName();
                using (BinaryWriter binaryWriter = new BinaryWriter(File.Open(tempFile, FileMode.Open)))
                {
                    binaryWriter.Write(Convert.FromBase64String(response.Data));
                }
                _currentMap = new Map(tempFile);
                return _currentMap;
            }
            return null;
        }

        public List<Rack> GetRackInfo()
        {
            var response = SendRequest(Command.GetRackInfo);
            if (response.IsValid)
            {
                _rackList = new List<Rack>();
                var dataSegment = response.Data.Split(',');
                var rackCount = int.Parse(dataSegment[0]);
                for (int i = 0; i < rackCount; i++)
                {
                    var offset = i * 4;
                    var rackName = dataSegment[offset + 1];
                    var re = new Regex(@"([a-zA-Z]+)(\d+)");
                    var rackID = int.Parse(re.Match(rackName).Groups[2].Value);
                    var currentX = int.Parse(dataSegment[offset + 3].Split('-')[1]);
                    var currentY = int.Parse(dataSegment[offset + 3].Split('-')[2]);
                    var homeX = int.Parse(dataSegment[offset + 2].Split('-')[1]);
                    var homeY = int.Parse(dataSegment[offset + 2].Split('-')[2]);
                    var rackHeading = (Rack.RackHeading)Enum.Parse(typeof(Rack.RackHeading), dataSegment[offset + 4]);
                    var newRack = new Rack(rackID, _currentMap.AllNodes[currentY, currentX], rackHeading, rackName, _currentMap.AllNodes[homeY, homeX]);
                    _rackList.Add(newRack);
                }
                return _rackList;
            }
            return null;
        }

        public bool AssignNewMission()
        {
            return false;
        }

        public void GetMissionState()
        {

        }

        public void CancelMission()
        {

        }

        public void GetCurrentMission()
        {

        }
    }
}
