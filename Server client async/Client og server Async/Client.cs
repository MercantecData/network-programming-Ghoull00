using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Client_og_server_Async
{
    class Client
    {
        public Client()
        {
            // Skriv den port og IPaddress vi ville connecte til som står ved serveren
            TcpClient client = new TcpClient();
            int port = 69;
            IPAddress ip = IPAddress.Parse("127.0.0.1");
            IPEndPoint endPoint = new IPEndPoint(ip, port);

            client.Connect(endPoint);
            NetworkStream stream = client.GetStream();
            ReciveMeseege(stream);

            // Her skriver vi en besked som bliver sent til stream.read ved ser
            Console.WriteLine("Du kan begynde at skrive en besked");
            while (true)
            {
                string Besked = Console.ReadLine();

                byte[] buffer = Encoding.UTF8.GetBytes(Besked);
                stream.Write(buffer, 0, buffer.Length);
            }
        }

        // Læser de beskeder der kommer gennem streamen
        public async void ReciveMeseege(NetworkStream stream)
        {
            byte[] bytes = new byte[256];
            while (true)
            {
                int messege = await stream.ReadAsync(bytes, 0, bytes.Length);
                string messegeGained = Encoding.UTF8.GetString(bytes, 0, messege);
                Console.Write("\n" + messegeGained);
            }
        }
    }
}
