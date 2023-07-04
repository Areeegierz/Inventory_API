using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inventory_API.Models.AD
{
    public class JsonViewModel
    {
        public int status { get; set; }
        public string message { get; set; }
        public DateTime timestamp { get; set; }
        public Datas data { get; set; }
        
    }
}
