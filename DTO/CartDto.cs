using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    [Serializable]
    public class CartDto
    {
        public string CustomerName { get; set; }

        public ICollection<CartRowDto> Rows { get; set; }
    }
}
