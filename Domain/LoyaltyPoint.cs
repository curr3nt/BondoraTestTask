using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class LoyaltyPoint
    {
        public int Id { get; set; }
        public InventoryType InventoryType { get; set; }
        public decimal Points { get; set; }
    }
}
