using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    [Serializable]
    public class EquipmentDto
    {
        public int Id { get; set; }
        [DisplayName("Equipment name")]
        public string Name { get; set; }
    }
}
