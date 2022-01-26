using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper.Configuration.Attributes;

namespace WMS.Models
{
    public class Material
    {        
        [Name("名稱","Name")]
        public string Name { get; set; }
        [Name("長", "Length")]
        public int Length { get; set; }
        [Name("寬", "Width")]
        public int Width { get; set; }
        [Name("料架", "Rack")]
        public string RackName { get; set; }
        [Name("層", "Layer")]
        public int Layer { get; set; }
        [Name("材料箱", "Box")]
        public int Box { get; set; }

        public override string ToString()
        {
            return this.Name;
        }
    }    
}
