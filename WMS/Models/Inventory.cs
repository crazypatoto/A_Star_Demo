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

        public override string ToString()
        {
            return this.Name;
        }
    }
}
