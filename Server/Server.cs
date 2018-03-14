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
        Queue<Message> messages;
        Dictionary<string, int> listOfClients;

        public Server()
        {
            server = new TcpListener(IPAddress.Parse("192.168.0.130"), 9999);
            server.Start();
            listOfClients = new Dictionary<string, int>();  ///added
            messages = new Queue<Message>();
        }
        public void Run()
        {
            Thread t = new Thread(AcceptClient);
            t.Start();

        }

        private void AcceptClient()     ////moved from bottom
        {
            while (true)
            {
                TcpClient clientSocket = default(TcpClient);
                clientSocket = server.AcceptTcpClient();
                Console.WriteLine("Connected");
                NetworkStream stream = clientSocket.GetStream();
                client = new Client(stream, clientSocket);
                listOfClients.Add(string, int);
                Thread threadMessage = new Thread(new ThreadStart(() => Message(client)));
                threadMessage.Start();
            }

        }

        public void Message(Client client)
        {
            while (true)
            {
                Message message = client.Recieve();
                messages.Enqueue(message);
            }
        }

        public void MessageSender(Client client)
        {
            while (true)
            {
                Message sendmeassage = messages.Dequeue();
                client.Send(sendmeassage);
                //Respond(message);
            }
        }



        private void Respond(Message body)
        {
             client.Send(body);   
        }


    }
}
