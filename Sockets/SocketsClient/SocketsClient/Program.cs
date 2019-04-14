using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;


namespace SocketsClient
{
    class SocketClient
    {
        public static void StartClient()
        {
  
            try
            {
 
                IPHostEntry Host = Dns.GetHostEntry(Dns.GetHostName());
                IPAddress Address = Host.AddressList[0];
                IPEndPoint EndPoint = new IPEndPoint(Address, 1700);

                // Create a socket.  
                Socket Client = new Socket(Address.AddressFamily,
                    SocketType.Stream, ProtocolType.Tcp);

                Client.Connect(EndPoint);

                String NewStudentRoster = "NewStudentRoster;StudID:111;Name:Bob Smith;SSN:222-333-1111;EmailAddress:bsmith@yahoo.com;HomePhone:215-777-8888;HomeAddr:123 Tulip Road, Ambler, PA 19002;LocalAddr:321 Maple Avenue, Lion Town, PA 16800;EmergencyContact:John Smith(215-222-6666);ProgramID):206;PaymentID:1111-206;AcademicStatus:1;";
                byte[] message = Encoding.Default.GetBytes(NewStudentRoster);
                Client.Send(message);

                int ReplyLen = Client.Receive(message);
                String reply = Encoding.Default.GetString(message, 0, ReplyLen);
                int result;

                if (Int32.TryParse(reply, out result) && result == 1 )
                    Console.WriteLine("Message successfully recieved by server.");

                Client.Shutdown(SocketShutdown.Both);
                Client.Close();

                Console.ReadLine();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public static int Main(String[] args)
        {
            StartClient();
            return 0;
        }

    }
}
