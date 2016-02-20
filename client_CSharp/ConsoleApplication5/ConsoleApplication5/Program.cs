using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;

namespace ConsoleApplication5
{
    class Program
    {
        static void Main(string[] args)
        {
            
            string ip = "127.0.0.1";
            int port = 1322;
            string message = "hello";

            Client client = new Client(ip,port);


            client.listenMesg();


            while(true)
            {

            message = Console.ReadLine();
            client.sendMessage(message);

            }

        }
    }
}
