using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Client_og_server_Async
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Client, UDPClient eller Server");

            while (true)
            {
                string choice = Console.ReadLine();
                if (choice == "Client")
                {
                 
                   Client client = new Client();
                }

                else if (choice == "UDPClient")
                {
                    UDP_Client UDPclient = new UDP_Client();
                }

                else if (choice == "Server")
                {
                    Server server = new Server();
                }

                else
                {
                    Console.WriteLine("Client, UDPClient eller Server");
                }
            }
        }
    }
}
