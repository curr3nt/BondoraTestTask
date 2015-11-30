using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic;

namespace BackendTcpService
{
    class Program
    {
        public static void Main()
        {
            // enables EF to consider app's directory as App_Data and ultimately store LocalDb file there
            AppDomain.CurrentDomain.SetData("DataDirectory", AppDomain.CurrentDomain.BaseDirectory);
            // TODO: ip / port to settings
            var server = new TcpServer("127.0.0.1", 10001, true, new BusinessService());
            server.Start();
        }
    }
}
