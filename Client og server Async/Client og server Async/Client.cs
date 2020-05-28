using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Client_og_server_Async
{
    class Client
    {
        string name = "ghft";
        public Client()
        {
            // connecter til serveren 
            TcpClient client = new TcpClient();

            Console.WriteLine("servern er på port 69");
            int port = Int32.Parse(Console.ReadLine());

            Console.WriteLine("IPaddresen er 127.0.0.1");
            string Ip = Console.ReadLine();
            IPAddress ip = IPAddress.Parse(Ip);

            IPEndPoint endPoint = new IPEndPoint(ip, port);
            client.Connect(endPoint);
            NetworkStream stream = client.GetStream();
            Console.WriteLine("Connected til en server");

            Console.WriteLine("Skriv dit navn");
            ReciveMessage(stream);

            // ville begynde på functionerne
            while (true)
            {
              
                SentMes(stream);
            }

        }
        // gør at man kan sende en besked
        public void SentMes(NetworkStream stream)
        {
            string brugernavn = Brugernavn();
            string tekst = Console.ReadLine();
            string tekst1 = brugernavn + ": " + tekst;

            byte[] buffer = Encoding.UTF8.GetBytes(tekst1);
            stream.Write(buffer, 0, buffer.Length);

        }
        public string Brugernavn()
        {
            if (name == "ghft")
            {
                name = Console.ReadLine();
            }
            return name;
        }

        // gør man ville kunne få en besked
        public async void ReciveMessage(NetworkStream stream)
        {
            while (true)
            {
                byte[] bytes = new byte[256];
                int NumberOfBitesRead = await stream.ReadAsync(bytes, 0, bytes.Length);
                string reciveMessage = Encoding.UTF8.GetString(bytes, 0, NumberOfBitesRead);

                Console.Write("\n" + reciveMessage);
            }
        }
    }
}
