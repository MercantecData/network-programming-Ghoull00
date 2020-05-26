using System;
using System.Net.Sockets;
using System.Net;
using System.Text;

namespace Client_og_server
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("skriv '1' for at bliver clienten og '2' for at blive serveren");
          string ok = Console.ReadLine();
            if(ok == "1")
            {
                Console.Clear();

                // Serveren starter op
                int portt = 13356;
                IPAddress ipp = IPAddress.Any;
                IPEndPoint localendpoint = new IPEndPoint(ipp, portt);

                TcpListener listener = new TcpListener(localendpoint);
                listener.Start();
                
                // Forbindelsen til servern
                TcpClient client = new TcpClient();

                int port = 13356;
                IPAddress ip = IPAddress.Parse("127.0.0.1");
                IPEndPoint endPoint = new IPEndPoint(ip, port);
                client.Connect(endPoint);

                //Her opretter vi beskeden til serveren
                NetworkStream stream = client.GetStream();

                Console.WriteLine("Skriv dit brugernavn");
                string Brugernavn = Console.ReadLine();

                TcpClient clientt = listener.AcceptTcpClient();
                NetworkStream streamm = clientt.GetStream();

                Console.WriteLine("Skriv din besked her til serveren");
                while (true)
                {
                    // Sender beskeden til serveren
                    string tekst = Console.ReadLine();
                    string Besked = Brugernavn + ": " + tekst;

                    byte[] buffer = Encoding.UTF8.GetBytes(Besked);
                    stream.Write(buffer, 0, buffer.Length);

                    byte[] bufferr = new byte[256];

                    // Får beskeden fra clienten til serveren
                    int numberOfBytesReadd = streamm.Read(bufferr, 0, 256);
                    string Beskeddd = Encoding.UTF8.GetString(buffer, 0, numberOfBytesReadd);
                    Console.WriteLine(Beskeddd);
                }
            }

            else if (ok == "2") 
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


                while (true)
                {
                    // Får beskeden fra klienten
                    int numberOfBytesRead = stream.Read(buffer, 0, 256);

                    string Besked = Encoding.UTF8.GetString(buffer, 0, numberOfBytesRead);
                    Console.WriteLine(Besked);


                    // Sender beskeden tilbage til klienten
                    byte[] bufffer = Encoding.UTF8.GetBytes(Besked);
                    stream.Write(bufffer, 0, bufffer.Length);
                }
            }

            else
            {
                Console.WriteLine("1 ELLER 2");
            }
        }
    }
}
