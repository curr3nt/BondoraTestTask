using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace BackendTcpService
{
    class Program
    {
        public static void Main()
        {
            var server = new TcpServer("127.0.0.1", 10001, false, null);
            server.Start();
        }
    }
}
