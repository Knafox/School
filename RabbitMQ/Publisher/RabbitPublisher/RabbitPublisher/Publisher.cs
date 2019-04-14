using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;

namespace RabbitPublisher
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.ExchangeDeclare("StudentInfo", ExchangeType.Topic, false, false, null);

                var message = GetMessage(args);
                var body = Encoding.UTF8.GetBytes(message.ToString());
                for (int i = 0; i < 25; i++)
                {
                    channel.BasicPublish("StudentInfo",
                                         "",
                                         null,
                                         body);
                    Console.WriteLine(" [x] Sent {0}", message);
                    System.Threading.Thread.Sleep(5000);
                }
            }

            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();
        }

        private static object GetMessage(string[] args)
        {
            
                return ((args.Length > 0)
                       ? string.Join(" ", args)
                       : "NewStudentRoster;StudID:111;Name:Bob Smith;SSN:222-333-1111;EmailAddress:bsmith@yahoo.com;HomePhone:215-777-8888;HomeAddr:123 Tulip Road, Ambler, PA 19002;LocalAddr:321 Maple Avenue, Lion Town, PA 16800;EmergencyContact:John Smith(215-222-6666);ProgramID):206;PaymentID:1111-206;AcademicStatus:1;");
            
        }
    }
}
