using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class InvoiceRow
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public virtual Equipment RentedEquipment { get; set; }
    }
}
