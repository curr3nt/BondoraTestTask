using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendTcpService.Protocol
{
    [Serializable]
    public class Message
    {
        public DateTime Timestamp { get; set; }
        public object Data { get; set; }

        public Message()
        {
            Timestamp = DateTime.Now;
        }
    }
}
