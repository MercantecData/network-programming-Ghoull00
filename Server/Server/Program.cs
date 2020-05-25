using System;
using System.Net.Sockets;
using System.Net;
using System.Text;
namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            int port = 13356;
            IPAddress ip = IPAddress.Any;
            IPEndPoint localendpoint = new IPEndPoint(ip, port);

            TcpListener listener = new TcpListener(localendpoint);
            listener.Start();

            Console.WriteLine("Venter på klient");
            TcpClient client = listener.AcceptTcpClient();

            NetworkStream stream = client.GetStream();

            byte[] buffer = new byte[256];
            int numberOfBytesRead = stream.Read(buffer, 0, 256);
            
            string Besked = Encoding.UTF8.GetString(buffer, 0, numberOfBytesRead);
            Console.WriteLine(Besked);
        }
    }
}
