using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BackendTcpService.Protocol;

namespace BackendTcpService
{
    public interface IBusinessService
    {
        Message ProcessMessage(Message message);
    }
}
