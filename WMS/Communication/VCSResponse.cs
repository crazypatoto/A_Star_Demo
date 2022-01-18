using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace WMS.Communication
{
    internal class VCSResponse
    {
        private string _rawResponse;
        public static readonly VCSResponse Empty = new VCSResponse();
        public DateTime Timestamp { get; }

        public Command Command { get; }

        public string Data { get; }

        public bool IsValid { get; }
        public VCSResponse(string response)
        {
            _rawResponse = response;
            var data = _rawResponse.Split(';');
            if (data.Length != 4)
            {
                this.IsValid = false;
                return;
            }

            try
            {
                this.Timestamp = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc).AddSeconds(long.Parse(data[0]));
                this.Command = (Command)Enum.Parse(typeof(Command), data[1]);
                this.Data = data[2];
            }
            catch (Exception ex)
            {
                this.IsValid = false;
                Debug.WriteLine(ex.Message);
                return;
            }
            this.IsValid = true;
        }

        public override string ToString()
        {
            return _rawResponse;
        }

        private VCSResponse()
        {
            this.IsValid = false;
        }
    }
}
