using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Web;
using BackendTcpService.Protocol;
using DTO;
using Frontend.CustomExceptions;

namespace Frontend.BackendWrapper
{
    public class BackendTcpServiceWrapper : IBackendServiceWrapper
    {
        // TODO: move ip / port to settings
        private readonly string _address;
        private readonly int _port;

        public BackendTcpServiceWrapper(string address, int port)
        {
            _address = address;
            _port = port;
        }

        public ICollection<EquipmentDto> GetInventory()
        {
            var message = new Message();
            message.Type = MessageType.GetInventory;

            var response = SendAndReceiveMessage(message);

            var inventory = response.Data as ICollection<EquipmentDto>;

            if (inventory == null)
                throw new BackendUnexpectedResponseDataException();

            return inventory;
        }

        private Message SendAndReceiveMessage(Message message)
        {
            TcpClient client = null;
            NetworkStream stream = null;
            Message response;
            try
            {
                client = new TcpClient(_address, _port);
                stream = client.GetStream();
                response = SendRequestAndProcessResponse(stream, message);
            }
            catch (Exception e)
            {
                throw new BackendUnavailableException(e);
            }
            finally
            {
                if (stream != null)
                    stream.Close();
                if (client != null)
                    client.Close();
            }
            return response;
        }

        private Message SendRequestAndProcessResponse(Stream stream, Message message)
        {
            // send message
            var formatter = new BinaryFormatter();
            formatter.Serialize(stream, message);

            // accept message
            var receivedObject = formatter.Deserialize(stream);
            var response = receivedObject as Message;
            
            if (response == null) 
                throw new BackendUnknownResponseException(receivedObject);
            
            if (message.Status == MessageStatus.Error)
                throw new BackendExceptionException();
                
            return response;
        }
    }
}