using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inventory_API.Models.AD
{
    public class Datas
    {
        public string username { get; set; }

        public string firstName{ get; set; }
        public string lastName{ get; set; }
        public string franchiseOwnerName{ get; set; }
        public string franchiseOwnerNo { get; set; }
        public string[] compcode { get; set; }
        public List<Division> divisions { get; set; }
        public List<Section> sections { get; set; }
        public List<Plant> plants { get; set; }
        public List<Department> departments { get; set; }
        public List<Dispatches> dispatches { get; set; }
        public List<SubDispatches> subDispatches { get; set; }
    }
}
