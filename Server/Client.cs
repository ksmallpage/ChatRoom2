using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Server
{
    class Client
    {
        NetworkStream stream;
        TcpClient client;
        public string UserId;
        public Client(NetworkStream Stream, TcpClient Client)
        {
            stream = Stream;
            client = Client;
            UserId = "495933b6-1762-47a1-b655-483510072e74";
            

        }
        public void Send(Message Message)
        {
            while (true)
            {
                byte[] message = Encoding.ASCII.GetBytes(Message.Body);
                stream.Write(message, 0, message.Count());
            }
            
        }
        public Message Recieve()
        {
            {
                byte[] recievedMessage = new byte[256];
                stream.Read(recievedMessage, 0, recievedMessage.Length);
                string recievedMessageString = Encoding.ASCII.GetString(recievedMessage);
                Console.WriteLine(recievedMessageString);
                Message message = new Message(this, recievedMessageString);
                return message;
            }
        }


        //queyuw

    }
}
