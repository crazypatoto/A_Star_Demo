using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net.Sockets;
using System.Net;
using System.Diagnostics;


namespace WMS.Communication
{
    public class VCSClient
    {
        private readonly int _port = 2312;
        private readonly int _sendTimeout = 10;
        private readonly int _receiveTimeout = 100;
        private readonly int _rxBuffeSize = 1024 * 1000; // 1MB
        private Socket _clientSocket;

        public VCSClient()
        {
            _clientSocket = new Socket(SocketType.Stream, ProtocolType.Tcp);
        }

        public bool Connect(string ip)
        {
            try
            {
                _clientSocket.Connect(new IPEndPoint(IPAddress.Parse(ip), _port));
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
                    return true;
                }
            }
            return false;
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
                    //_clientSocket.ReceiveTimeout = _receiveTimeout;
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

        public void GetMapData()
        {
            var response = SendRequest(Command.GetMapData);
            if (response.IsValid)
            {
                Debug.WriteLine(response.Data);
            }
        }

        public void GetRackInfo()
        {
            var response = SendRequest(Command.GetRackInfo);
            if (response.IsValid)
            {

            }
        }
    }
}
