using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper.Configuration.Attributes;

namespace WMS.Models
{
    public class RackInfo
    {
        public static readonly int RackLength = 100;
        public static readonly int RackWidth = 100;
        [Name("料架")]
        public string RackName { get; set; }
        [Name("總層數")]
        public int LayerCount { get; set; }
        [Name("材料箱長")]
        public int BoxLength { get; set; }
        [Name("材料箱寬")]
        public int BoxWidth { get; set; }    
        public int BoxCountPerLayer { get { return 2 * (int)(RackLength / this.BoxLength) + 2 * (int)(RackWidth / this.BoxWidth) - 4; } }
        public override string ToString()
        {
            return this.RackName;
        }
    }
}
