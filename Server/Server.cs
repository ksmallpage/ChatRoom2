using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Server
{
    class Server
    {
        public static Client client;
        TcpListener server;
        public Server()
        {
         
            server = new TcpListener(IPAddress.Parse("192.168.0.130"), 9999);
            server.Start();
            Thread t = new Thread(Run);  //
            t.Start();

        }
        public  void Run()  //
        {
            while (true)
            {
                AcceptClient();
                Message message = client.Recieve();
                Respond(message); // add message
            }
            
        }
        private void AcceptClient()
        {
                TcpClient clientSocket = default(TcpClient);
                clientSocket = server.AcceptTcpClient();
                Console.WriteLine("Connected");
                NetworkStream stream = clientSocket.GetStream();
                client = new Client(stream, clientSocket);
        }

        private void Respond(Message body)
        {
              
             client.Send(body);   
        }


    }
}
