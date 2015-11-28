using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using BackendTcpService;
using BackendTcpService.Protocol;

namespace TestApplication
{
    class Program
    {
        public static void Main()
        {
            var message = new Message();
            message.Data = "Hello, service";

            TcpClient client = new TcpClient("127.0.0.1", 10001); // have my connection established with a Tcp Server 

            IFormatter formatter = new BinaryFormatter(); // the formatter that will serialize my object on my stream 

            NetworkStream strm = client.GetStream(); // the stream 
            formatter.Serialize(strm, new object()); // the serialization process 

            strm.Close();
            client.Close();
        } 
    }
}
