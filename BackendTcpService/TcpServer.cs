using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using BackendTcpService.Protocol;
using BusinessLogic;
using DTO;
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
                    ProcessIncomingRequestAndSendResponse(stream);

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
        /// Method checks if object of type Message was indeed written to the stream by a client,
        /// creates a response Message.
        /// </summary>
        /// <param name="stream"></param>
        private void ProcessIncomingRequestAndSendResponse(Stream stream)
        {
            var response = new Message();
            // accept Message
            var formatter = new BinaryFormatter();
            var receivedObject = formatter.Deserialize(stream);
            var message = receivedObject as Message;
            if (message != null)
            {
                try
                {
                    response = ProcessMessage(message);
                }
                catch (Exception e)
                {
                    Logger.Error(e);
                    response.Data = "An error has occured on the service-side";
                    response.Status = MessageStatus.Error;
                }
            }
            else
            {
                var errorMessage = string.Format("Received unknown object; Type: {0}, ToString(): {1}",
                    receivedObject.GetType(), receivedObject);
                Logger.Warn(errorMessage);
                response.Data = errorMessage;
                response.Status = MessageStatus.Error;
            }
            // send Message
            formatter.Serialize(stream, response);
        }

        private Message ProcessMessage(Message message)
        {
            var response = new Message();
            switch (message.Type)
            {
                case MessageType.GetInventory:
                    // get equipment list
                    response.Data = _service.GetInventoryList();
                    response.Status = MessageStatus.Success;
                    break;
                case MessageType.ConfirmCart:
                    // create invoice, calculate prices and loyalty points
                    var cart = message.Data as CartDto;
                    if (cart != null)
                    {
                        response.Data = _service.ConfirmCart(cart);
                        response.Status = MessageStatus.Success;
                    }
                    else
                    {
                        response.Data = string.Format("Message data is unknown; Type: {0}, ToString(): {1}",
                            message.Data.GetType(), message.Data);
                        response.Status = MessageStatus.Error;
                    }
                    break;
                default:
                    response.Data = string.Format("Unknown message type: {0}", message.Type);
                    response.Status = MessageStatus.Error;
                    break;
            }
            return response;
        }
    }
}
