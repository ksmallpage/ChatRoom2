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
         
            server = new TcpListener(IPAddress.Parse("192.168.0.112"), 9999);
            server.Start();
            

        }
        public void Run()
        {
            Thread t = new Thread(AcceptClient);
            t.Start();
            while (true)
            {
                AcceptClient();

            }
        }

            public void Message(Client client)
        {
            while (true)
            {
                string message = client.Recieve();
                Respond(message);
            }
            
        }
  
        
        private  void AcceptClient()
        {
                
            
                TcpClient clientSocket = default(TcpClient);
            
                clientSocket = server.AcceptTcpClient();
                Console.WriteLine("Connected");
                NetworkStream stream = clientSocket.GetStream();
                client = new Client(stream, clientSocket);
                Thread threadMessage = new Thread( new ThreadStart( () =>  Message(client)));
                 threadMessage.Start();

        }
        private void Respond(string body)
        {
              
             client.Send(body);   
        }
    }
}
