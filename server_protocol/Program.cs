using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace server_protocol
{
    class Program
    {
        static void Main(string[] args)
        {
            IPHostEntry ipHost = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress ip = ipHost.AddressList[0];
            IPEndPoint endpoint = new IPEndPoint(ip, 8080);

            Socket receiver = new Socket(ip.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            byte[] bytes = new byte[1024];
            string message = null;

            try {
                receiver.Bind(endpoint);    //Throws exception if address/port already in use
                Console.WriteLine("Server bound to EP");
                receiver.Listen(5);   //Will only allow 20 clients in communication queue

                while (true)
                {
                    Socket clientHandler = receiver.Accept();
                    message = null;

                    while (true)
                    {
                        int recieved = clientHandler.Receive(bytes);
                        message += ASCIIEncoding.ASCII.GetString(bytes, 0, recieved);
                        if (message.IndexOf("<EOF>") > -1)
                        {
                            break;
                        }
                    }
                    Console.WriteLine(message);
                    // Echo the data back to the client.  
                    byte[] msg = Encoding.ASCII.GetBytes("Delivered");
                    clientHandler.Send(msg);
                    Console.Read();
                }

                receiver.Shutdown(SocketShutdown.Both);
                receiver.Close();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                Console.Read();
            }
        }
    }
}
