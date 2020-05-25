using System;
using System.Text;

namespace Program_netværk
{
    class Program
    {
        static void Main(string[] args)
        {

            String text = "Håber du okay";

            Byte[] bytes = Encoding.ASCII.GetBytes(text);
            foreach(Byte b in bytes)
            {
                Console.WriteLine(b);
            }

            string convert = Encoding.ASCII.GetString(bytes);
            Console.WriteLine(convert);

            Console.WriteLine("");
            
            Byte[] bytes1 = Encoding.UTF8.GetBytes(text);
            foreach (Byte b in bytes1)
            {
                Console.WriteLine(b);
            }

            string convert1 = Encoding.UTF8.GetString(bytes1);
            Console.WriteLine(convert1);
        }
    }
}
