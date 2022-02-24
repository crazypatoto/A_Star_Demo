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
            NoRestrictions = 0b0000,
            NoLeaving = 0b0001,
            NoEntering = 0b0010,
            NoEnterOrLeaving = 0b0011,
        }

        public const int RawBytesLength = 6;

        public PassingRestrictions PassingRestriction { get; set; }
        public float SpeedLimit { get; set; }
        public bool IsBounded { get; set; }
        public MapEdge()
        {
            this.PassingRestriction = PassingRestrictions.NoRestrictions;
            this.SpeedLimit = 0;
            this.IsBounded = false;
        }

        public MapEdge(PassingRestrictions passingResrtiction, float speedLimit, bool isBounded)
        {
            this.PassingRestriction = passingResrtiction;
            this.SpeedLimit = speedLimit;
            this.IsBounded = isBounded;
        }

        public MapEdge(byte[] byteArray, int index = 0)
        {
            this.PassingRestriction = (PassingRestrictions)byteArray[index + 0];
            this.SpeedLimit = BitConverter.ToSingle(byteArray, index + 1);
            this.IsBounded = BitConverter.ToBoolean(byteArray, index + 5);
        }

        public byte[] ToBytes()
        {
            byte[] byteArray = new byte[RawBytesLength];
            byteArray[0] = (byte)PassingRestriction;
            Array.Copy(BitConverter.GetBytes(this.SpeedLimit), 0, byteArray, 1, 4);
            Array.Copy(BitConverter.GetBytes(this.IsBounded), 0, byteArray, 5, 1);
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
