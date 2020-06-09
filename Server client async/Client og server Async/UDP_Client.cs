using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Net;

namespace Client_og_server_Async
{
    public class UDP_Client
    {
        public UDP_Client()
        {
            // Skriv den port og IPaddress vi ville connecte til som står ved serveren
            UdpClient client = new UdpClient();
            int port = 5000;
            IPAddress ip = IPAddress.Parse("127.0.0.1");
            IPEndPoint endPoint = new IPEndPoint(ip, port);
            ReciveMeseege();

            // Her skriver vi en besked som bliver sent til stream.read ved ser
            Console.WriteLine("Du kan begynde at skrive en besked");
            while (true)
            {
                string Besked = Console.ReadLine();

                byte[] buffer = Encoding.UTF8.GetBytes(Besked);
                client.Send(buffer, buffer.Length, endPoint);
            }
        }

        // Læser de beskeder der kommer gennem streamen
        public async void ReciveMeseege()
        {
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 5000);
            UdpClient client = new UdpClient(endPoint);
            while (true)
            {
                UdpReceiveResult result = await client.ReceiveAsync();

                byte[] buffer = result.Buffer;
                string text = Encoding.UTF8.GetString(buffer);

                Console.WriteLine("recive " + text);
            }
        }
    }
  
}
