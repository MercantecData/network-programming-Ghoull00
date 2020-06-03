using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Diagnostics.SymbolStore;
using System.Runtime.InteropServices.ComTypes;
using System.Threading;
using System.Globalization;

namespace Client_og_server_Async
{
    // vi laver en class til så vi kan oprette et brugernavn
    public class Brugernavn
    {
        public string brugernavn;
        public TcpClient client;
        
        public Brugernavn(string brugernavn, TcpClient client)
        {
            this.brugernavn = brugernavn;
            this.client = client;
        }
    }

    class Server
    {
        bool Spil = true;
        bool Teskst = true;
        bool Teskst1 = true;

        public List<Brugernavn> clients = new List<Brugernavn>();

        public Server()
        {
            Console.WriteLine("Skriv 'spil' eller 'tekst'");
            string ok = Console.ReadLine();
            if (ok == "spil")
            {
                Spil = true;
                Teskst = false;
                Teskst1 = false;

            }
            else if (ok == "tekst")
            {
                Teskst = true;
                Spil = false;
                Teskst1 = true;
            }

            // Opretter en server 
            int port = 69;
            IPAddress ip = IPAddress.Any;
            IPEndPoint endPoint = new IPEndPoint(ip, port);

            // Activere functionen 'AcceptClient'
            TcpListener listener = new TcpListener(endPoint);
            listener.Start();
            AcceptClient(listener);

            // Serveren skriver beskeder til clietnerne
            while (Teskst1)
            {
                string Besked = Console.ReadLine();
                string Besked1 = "Server: " + Besked;
                byte[] bytes = Encoding.UTF8.GetBytes(Besked1);

                foreach (Brugernavn brugernavn in clients)
                {
                    brugernavn.client.GetStream().Write(bytes, 0, bytes.Length);
                }
               
            }
        }

        // Her viller serveren høre efter hvis der er clienter der joiner serveren
        public async void AcceptClient(TcpListener listener)
        {

            Console.WriteLine("Venter på client");
            byte[] bytes = new byte[256];
            while (true)
            {
                TcpClient client = await listener.AcceptTcpClientAsync();
                
                NetworkStream stream = client.GetStream();
                int okay = stream.Read(bytes, 0, bytes.Length);
                string ok = Encoding.UTF8.GetString(bytes, 0, okay);

                clients.Add(new Brugernavn(ok, client));
                Console.WriteLine("En client har joined");

                ReciveMessages(stream, ok);
            }
        }

        // Gør at serveren kan læse beskeder fra clienterne
        public async void ReciveMessages(NetworkStream stream, string navn)
        {
            byte[] buffer = new byte[256];

            Random random = new Random();
            int Randomnumber = random.Next(0, 100);
            Console.WriteLine(Randomnumber);

            // Her er loobet for når man ville have servern til at køre en chat HUSK AT SLÅ ANTIVIRUS FRA DIN ABE
            while (Teskst)
            {
                int BeskedeSendt = await stream.ReadAsync(buffer, 0, buffer.Length);
                string text = Encoding.UTF8.GetString(buffer, 0, BeskedeSendt);
               
                if (Teskst == true)
                {
                    byte[] bytte = Encoding.UTF8.GetBytes(navn + ": " + text);
                    foreach (Brugernavn brugernavn in clients)
                    {
                        brugernavn.client.GetStream().Write(bytte, 0, bytte.Length);
                    }
                    Console.WriteLine(navn + ": " + text);

                }
                else
                {
                    Console.WriteLine(text);
                }
                
                byte[] bytes = Encoding.UTF8.GetBytes(text + "\n");
               
            }
            // Her er while loobet der ville køre når man starter serveren og vælger hvad der skal starte op
            while (Spil)
            {
                int svar = await stream.ReadAsync(buffer, 0, buffer.Length);
                string svaret = Encoding.UTF8.GetString(buffer, 0, svar);
                int nul;

                bool okayt = Int32.TryParse(svaret, out nul);

                // Her har vi vores if statement for om hvor tæt tallet er på det der er mellem 1-100
                if (okayt)
                {
                    if (nul < Randomnumber)
                    {
                        byte[] bytes = Encoding.UTF8.GetBytes("tallet er Højre" + "\n" + "sidste gæt = " + nul + "\n");
                        foreach (Brugernavn brugernavn in clients)
                        {
                            brugernavn.client.GetStream().Write(bytes, 0, bytes.Length);

                        }
                    }

                    else if (nul > Randomnumber)
                    {
                        byte[] bytes = Encoding.UTF8.GetBytes("tallet er Laver" + "\n" + "sidste gæt = " + nul + "\n");
                        foreach (Brugernavn brugernavn in clients)
                        {
                            brugernavn.client.GetStream().Write(bytes, 0, bytes.Length);
                        }
                    }

                    else
                    {
                        byte[] bytes = Encoding.UTF8.GetBytes("tallet er Rigtigt" + "\n" + "sidste gæt = " + nul + "\n");
                        foreach (Brugernavn brugernavn in clients)
                        {
                            brugernavn.client.GetStream().Write(bytes, 0, bytes.Length);
                        }
                    }
                }
            }
        }
    }
}
