using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using BackendTcpService;
using BackendTcpService.Protocol;
using DAL;
using Domain;

namespace TestApplication
{
    class Program
    {
        static void Main()
        {
            using (var db = new DalContext())
            {
                AppDomain.CurrentDomain.SetData("DataDirectory", AppDomain.CurrentDomain.BaseDirectory);

                // Create and save a new equipment 
                //Console.Write("Enter a name for a new Equipment: ");
                //var name = Console.ReadLine();

                //var equipment = new Equipment { Name = name };
                //db.Equipments.Add(equipment);
                //db.SaveChanges();

                // Display all equipments from the database 
                var query = from b in db.Equipments
                            orderby b.Name
                            select b;

                Console.WriteLine("All equipment in the database:");
                foreach (var item in query)
                {
                    Console.WriteLine(item.Name + "\t" + item.Type);
                }

                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();
            }
        }

        static void Main1()
        {
            var message = new Message();
            message.Data = "Hello, service";

            TcpClient client = new TcpClient("127.0.0.1", 10001); // have my connection established with a Tcp Server 

            IFormatter formatter = new BinaryFormatter(); // the formatter that will serialize my object on my stream 

            NetworkStream strm = client.GetStream(); // the stream 
            formatter.Serialize(strm, message); // the serialization process 
            //formatter.Serialize(strm, new object()); // the serialization process 

            strm.Close();
            client.Close();
        } 
    }
}
