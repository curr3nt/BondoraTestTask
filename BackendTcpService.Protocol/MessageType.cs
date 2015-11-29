using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendTcpService.Protocol
{
    [Serializable]
    public enum MessageType
    {
        Unknown = 0,
        GetInventory = 1,
        ConfirmCart = 2
    }
}
