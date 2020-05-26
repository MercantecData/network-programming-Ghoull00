using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Client_async
{
    class Program
    {
        static void Main(string[] args)
        {
            
            TcpClient client = new TcpClient();

            int port = 13356;
            IPAddress ip = IPAddress.Parse("127.0.0.1");
            IPEndPoint endPoint = new IPEndPoint(ip, port);

            client.Connect(endPoint);

            NetworkStream stream = client.GetStream();
            ReciveMessage(stream);

            Console.Write("Write your message here");
            string text = Console.ReadLine();
            byte[] buffer = Encoding.UTF8.GetBytes(text);

            stream.Write(buffer, 0, buffer.Length);

            client.Close();

            static async void ReciveMessage(NetworkStream stream)
            {
            while (true)
            {
                byte[] buffer = new byte[256];

                int numberOfButeRead = await stream.ReadAsync(buffer, 0, 256);
                string reciveMessage = Encoding.UTF8.GetString(buffer, 0, numberOfButeRead);

                Console.Write("\n" +reciveMessage);
            }
                   
            }
        }
    }
}