using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace BusinessLogic
{
    public interface IBusinessService
    {
        ICollection<EquipmentDto> GetInventoryList();
        InvoiceFileDto ConfirmCart(CartDto cart);
    }
}
