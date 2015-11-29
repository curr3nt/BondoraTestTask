using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    [Serializable]
    public class CartRowDto
    {
        public int EquipmentId { get; set; }
        public int DaysRented { get; set; }
    }
}
