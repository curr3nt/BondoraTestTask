using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Equipment
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public InventoryType Type { get; set; }
    }
}
