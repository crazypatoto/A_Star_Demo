using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VCS.Maps
{
    public class MapEdge
    {     
        [Flags]
        public enum PassingRestrictions : byte
        {
            NoRestrictions = 0x00,
            NoLeaving = 0x01,
            NoEntering = 0x02,
            NoEnterOrLeaving = 0x03
        }

        public const int RawBytesLength = 5;

        public PassingRestrictions PassingRestriction { get; set; }
        public float SpeedLimit { get; set; }

        public MapEdge()
        {
            this.PassingRestriction = PassingRestrictions.NoRestrictions;
            this.SpeedLimit = 0;            
        }

        public MapEdge(PassingRestrictions passingResrtiction, float speedLimit)
        {
            this.PassingRestriction = passingResrtiction;
            this.SpeedLimit = speedLimit;
        }

        public MapEdge(byte[] byteArray, int index = 0)
        {
            this.PassingRestriction = (PassingRestrictions)byteArray[index + 0];
            this.SpeedLimit = BitConverter.ToSingle(byteArray, index + 1);
        }

        public byte[] ToBytes()
        {
            byte[] byteArray = new byte[RawBytesLength];
            byteArray[0] = (byte)PassingRestriction;
            Array.Copy(BitConverter.GetBytes(this.SpeedLimit), 0, byteArray, 1, 4);                        
            return byteArray;
        }

        public void InvertPassingRestrictionDirection()
        {
            if (this.PassingRestriction == PassingRestrictions.NoRestrictions || this.PassingRestriction == PassingRestrictions.NoEnterOrLeaving) return;
            var inverse = (~((byte)this.PassingRestriction)) & 0x03;
            this.PassingRestriction = (MapEdge.PassingRestrictions)inverse;
        }
    }
}
