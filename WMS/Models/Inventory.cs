using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper.Configuration.Attributes;

namespace WMS.Models
{
    public class Inventory
    {
        [Name("名稱", "Name")]
        public string Name { get; set; }
        [Name("數量", "Quantity")]
        public int Quantity { get; set; }
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
