using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace Frontend.BackendWrapper
{
    public interface IBackendServiceWrapper
    {
        ICollection<EquipmentDto> GetInventory();
        InvoiceFileDto ConfirmCart(CartDto cart);
    }
}
