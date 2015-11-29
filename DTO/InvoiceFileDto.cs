using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    [Serializable]
    public class InvoiceFileDto
    {
        public ICollection<string> FileRows { get; set; }
    }
}
