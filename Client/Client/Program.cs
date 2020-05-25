using System;
using System.Net.Sockets;
using System.Net;
using System.Text;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            // Forbindelsen til servern
            TcpClient client = new TcpClient();
            
            int port = 13356;
            IPAddress ip = IPAddress.Parse("127.0.0.1");
            IPEndPoint endPoint = new IPEndPoint(ip, port);

            client.Connect(endPoint);

            //Her opretter vi beskeden til serveren
            NetworkStream stream = client.GetStream();

            string Besked = "Test";
            byte[] buffer = Encoding.UTF8.GetBytes(Besked);

            stream.Write(buffer, 0, buffer.Length);
            client.Close();
        }
    }
}
