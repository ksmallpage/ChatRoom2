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
<<<<<<< HEAD
        Dictionary<int, Client> listOfClients;
        int key;

        public Server()
        {
            server = new TcpListener(IPAddress.Parse("192.168.0.135"), 9999);
=======
        private string clientName;

        public Server()
        {

            server = new TcpListener(IPAddress.Parse("192.168.0.133"), 9999);
>>>>>>> 0180b6e1862bda97015b3b11d2b08c00d7fb4cfd
            server.Start();
            listOfClients = new Dictionary<int, Client>();  ///added
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

        public void SendMessager()
        {
            while (true)
            {
                Message sendmeassage = messages.Dequeue();
                client.Send(sendmeassage);
            }
        }

        public void MessageSender(Client client)
        {
            while (true)
            {
<<<<<<< HEAD
                Message sendmeassage = messages.Dequeue();
                client.Send(sendmeassage);
                //Respond(message);
            }
=======
                TcpClient clientSocket = default(TcpClient);
                Console.WriteLine("Awaiting for client Connection");
                clientSocket = server.AcceptTcpClient();
                Console.WriteLine("what is your Name?");
                string clientName = Console.ReadLine();
                NetworkStream stream = clientSocket.GetStream();
                client = new Client(stream, clientSocket, clientName);
                Thread threadMessage = new Thread(new ThreadStart(() => Message(client)));
                threadMessage.Start();
            }         

>>>>>>> 0180b6e1862bda97015b3b11d2b08c00d7fb4cfd
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
