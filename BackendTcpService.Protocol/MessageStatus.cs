using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendTcpService.Protocol
{
    [Serializable]
    public enum MessageStatus
    {
        Success = 0,
        Error = 1
    }
}
