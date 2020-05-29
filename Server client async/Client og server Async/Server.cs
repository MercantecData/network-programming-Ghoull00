using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Client_og_server_Async
{
    
    class Server
    {
        public List<TcpClient> clients = new List<TcpClient>();
        
        public Server()
        {
            // Opretter en server 
            int port = 69;
            IPAddress ip = IPAddress.Any;
            IPEndPoint endPoint = new IPEndPoint(ip, port);

            // Activere functionen 'AcceptClient'
            TcpListener listener = new TcpListener(endPoint);
            AcceptClient(listener);

            // Skriver en besked som clienter kan se
            while (true)
            {
                string Besked = Console.ReadLine();
                string Besked1 = "Server: " + Besked;
                byte[] bytes = Encoding.UTF8.GetBytes(Besked1);

                foreach(TcpClient client in clients)
                {
                    client.GetStream().Write(bytes, 0, bytes.Length);
                }
            }
        }
         
        // Her viller serveren høre efter hvis der er clienter der joiner serveren
        public async void AcceptClient(TcpListener listener)
        {
            Console.WriteLine("Venter på client");
            while (true)
            {
                listener.Start();
                TcpClient client = await listener.AcceptTcpClientAsync();
                clients.Add(client);
               // Console.WriteLine("En client har joined serveren");

                NetworkStream stream = client.GetStream();
                ReciveMessages(stream);
            }
        }

        // Gør at serveren kan læse beskeder fra clienterne
        public async void ReciveMessages(NetworkStream stream)
        {
            byte[] buffer = new byte[256];
            while(true)
            {
                int BeskedeSendt = await stream.ReadAsync(buffer, 0, buffer.Length);
                string text = Encoding.UTF8.GetString(buffer, 0, BeskedeSendt);
                Console.WriteLine(text);
            }
        }
    }
}
