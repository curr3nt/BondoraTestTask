using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    [Serializable]
    public class CartRowDto
    {
        public int EquipmentId { get; set; }
        [DisplayName("Equipment name")]
        public string EquipmentName { get; set; }
        [DisplayName("Number of rental days")]
        public int DaysRented { get; set; }
    }
}
