﻿using System;
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

        public Server()
        {

            server = new TcpListener(IPAddress.Parse("192.168.0.112"), 9999);
            server.Start();
            messages = new Queue<Message>();
        }
        public void Run()
        {
            Thread t = new Thread(AcceptClient);
            t.Start();

        }

        public void Message(Client client)
        {
            while (true)
            {
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

        private void AcceptClient()
        {
            while (true)
            {
                TcpClient clientSocket = default(TcpClient);
                clientSocket = server.AcceptTcpClient();
                Console.WriteLine("Connected");
                NetworkStream stream = clientSocket.GetStream();
                client = new Client(stream, clientSocket);
                Thread threadMessage = new Thread(new ThreadStart(() => Message(client)));
                threadMessage.Start();
            }         

        }

        private void Respond(Message body)
        {
              
             client.Send(body);   
        }


    }
}
