using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Text.RegularExpressions;

namespace SocketsServer
{
    class Server
    {
        public static void StartServer()
        {
            try
            {
                IPHostEntry Host = Dns.GetHostEntry(Dns.GetHostName());
                IPAddress Address = Host.AddressList[0];
                IPEndPoint EndPoint = new IPEndPoint(Address, 1700);

                // Create a socket.  
                Socket Server = new Socket(Address.AddressFamily,
                SocketType.Stream, ProtocolType.Tcp);

                Server.Bind(EndPoint);
                Server.Listen(10);
                
                //wait for activity loop
                while (true)
                {
                    Console.WriteLine("Server Started, awaiting connection...");
                    Socket connection = Server.Accept();

                    byte[] message = new Byte[1048];
                    int messageLen = connection.Receive(message);
                    int returnMessage = 0;

                    try
                    {
                        returnMessage = ParseMessage(Encoding.Default.GetString(message, 0, messageLen));
                    }
                    finally
                    {
                        // return default if no success was found, telling the client to try again
                        connection.Send(Encoding.Default.GetBytes(returnMessage.ToString()));
                        connection.Shutdown(SocketShutdown.Both);
                        connection.Close();
                    }
                }
            }            
            catch(Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public static int ParseMessage(String message)
        {
            Console.WriteLine(message);
            
            //Parse Message into Object
            //Add object to storage
            
            //return success
            return 1;
        }


        public static int Main(string[] args)
        {
       
            StartServer();
            return 0;
        }
    }
}
