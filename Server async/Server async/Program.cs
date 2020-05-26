using System;
using System.Collections.Generic;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;

namespace Server_async
{
    class Program
    {
        static void Main(string[] args)
        {
           
                int port = 13356;
                IPAddress ip = IPAddress.Any;
                IPEndPoint loacalEndpoint = new IPEndPoint(ip, port);

                TcpListener listener = new TcpListener(loacalEndpoint);
                listener.Start();

                Console.WriteLine("Awaits clients");
                TcpClient client = listener.AcceptTcpClient();

                NetworkStream stream = client.GetStream();
                ReciveMessage(stream);

                Console.Write("Write your message");
                string text = Console.ReadLine();
                byte[] buffer = Encoding.UTF8.GetBytes(text);

                stream.Write(buffer, 0, buffer.Length);

                Console.ReadKey();

                static async void ReciveMessage(NetworkStream stream)
                {
                    byte[] buffer = new byte[256];

                    int numberOfButeRead = await stream.ReadAsync(buffer, 0, 256);
                    string reciveMessage = Encoding.UTF8.GetString(buffer, 0, numberOfButeRead);

                    Console.Write(reciveMessage);
                }
            

        }


    }

}

