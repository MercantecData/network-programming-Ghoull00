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
        public Server()
        {
            // opretter en server 
            int port = 69;
            IPAddress ip = IPAddress.Any;
            IPEndPoint endpoint = new IPEndPoint(ip, port);

            TcpListener listener = new TcpListener(endpoint);
            Task<NetworkStream> stream = Connect(listener);

            NetworkStream stream1 = stream.Result;

            ReciveMessage(stream1);

            // ville begynde på functionerne
            while (true)
            {
                SentMes(stream1);
            }
        }

        // Lytter for clienter der ville joine serveren
        public async Task<NetworkStream> Connect(TcpListener listener)
        {
            
            listener.Start();
            Console.WriteLine("Awaiting clients");
            TcpClient client = await listener.AcceptTcpClientAsync();
            Console.WriteLine("A client joined");

            NetworkStream stream = client.GetStream();
            return stream;
        }

        // gør at man kan sende en besked
        public void SentMes(NetworkStream stream)
        {
            string tekst = Console.ReadLine();
            byte[] buffer = Encoding.UTF8.GetBytes(tekst);
            stream.Write(buffer, 0, buffer.Length);
        }

        // gør man ville kunne få en besked
        public async void ReciveMessage(NetworkStream stream)
        {

            while(true)
            {
                byte[] bytes = new byte[256];
                int NumberOfBitesRead = await stream.ReadAsync(bytes, 0, bytes.Length);
                string reciveMessage = Encoding.UTF8.GetString(bytes, 0, NumberOfBitesRead);

                Console.Write("\n" + reciveMessage);
            }
           
        }
    }
}
