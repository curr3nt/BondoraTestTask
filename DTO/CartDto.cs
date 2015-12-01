using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    [Serializable]
    public class CartDto
    {
        [Required]
        [DisplayName("Customer")]
        public string CustomerName { get; set; }

        public IList<CartRowDto> Rows { get; set; }
    }
}
