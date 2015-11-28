using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using BackendTcpService.Protocol;
using NLog;

namespace BackendTcpService
{
    public class TcpServer
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private readonly string _address;
        private readonly int _port;
        private volatile bool _keepAlive;
        private readonly IBusinessService _service;

        public TcpServer(string address, int port, bool keepAlive, IBusinessService service)
        {
            _address = address;
            _port = port;
            _keepAlive = keepAlive;
            _service = service;
        }

        public void Start()
        {
            TcpListener listener = null;
            try
            {
                var address = IPAddress.Parse(_address);
                listener = new TcpListener(address, _port);

                listener.Start();

                Logger.Info("The server is running at: " + listener.LocalEndpoint);

                ListenForClients(listener);
            }
            catch (Exception e)
            {
                Logger.Fatal(e);
            }
            finally
            {
                Logger.Info("Server has stopped");
                if (listener != null)
                    listener.Stop();
            }
        }

        private void ListenForClients(TcpListener listener)
        {
            do
            {
                TcpClient client = null;
                NetworkStream stream = null;
                try
                {
                    client = listener.AcceptTcpClient();
                    Logger.Debug("Incoming connection from: " + client.Client.RemoteEndPoint);
                    stream = client.GetStream();
                    ProcessIncomingRequest(stream);

                    stream.Close();
                    client.Close();
                }
                catch (Exception e)
                {
                    Logger.Error(e);
                }
                finally
                {
                    if (stream != null)
                        stream.Close();
                    if (client != null)
                        client.Close();
                }
            } while (_keepAlive);
        }

        /// <summary>
        /// Mehtod checks if object of type Message was indeed written to the stream by a client
        /// </summary>
        /// <param name="stream"></param>
        private void ProcessIncomingRequest(NetworkStream stream)
        {
            var formatter = new BinaryFormatter();
            var receivedObject = formatter.Deserialize(stream);
            var message = receivedObject as Message;
            if (message != null)
                _service.ProcessMessage(message);
            else
                Logger.Warn("Received unknown object; Type: {0}, ToString(): {1}",
                    receivedObject.GetType(), receivedObject);
        }
    }
}
