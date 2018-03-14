using System;
using System.Collections;
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
        Dictionary<int, Client> listOfClients;
        int key;

        public Server()
        {
            server = new TcpListener(IPAddress.Parse("192.168.0.135"), 9999);
            server.Start();
            listOfClients = new Dictionary<int, Client>();
            messages = new Queue<Message>();
        }

        public void Run()
        {
            Thread t = new Thread(AcceptClient);
            t.Start();

        }

        private void AcceptClient()
        {
            while (true)
            {
                TcpClient clientSocket = default(TcpClient);
                clientSocket = server.AcceptTcpClient();
                Console.WriteLine("Connected");
                NetworkStream stream = clientSocket.GetStream();
                client = new Client(stream, clientSocket);
                key++;
                captureClient(key, client);
                Thread threadMessage = new Thread(new ThreadStart(() => Message(client))); //thread
                threadMessage.Start();
            }

        }
        public void Message(Client client)
        {
            while (true)
            {
                //create thread
                Message message = client.Recieve();
                messages.Enqueue(message);
                Respond(message);
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

        public void captureClient(int key, Client client)
        {
        listOfClients.Add(key, client);
        }
    }

}





