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

        public override string ToString()
        {
            return this.Name;
        }
    }    
}
