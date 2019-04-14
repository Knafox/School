using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;
using StudentInfo;

namespace StudentInfoConsumer
{
    class Program
    {
        const string connectionString = "Endpoint=sb://nf-esb.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=bpy5c5yodPL/gJ7oUoRL3bxDMu433dNCQ4Hjj8VvolQ=";
        const string queueName = "studentinfoqueue";
        static IQueueClient queueClient;

        static void Main(string[] args)
        {
            SendStudentInfo().GetAwaiter().GetResult();
        }

        static async Task SendStudentInfo()
        {
            queueClient = new QueueClient(connectionString, queueName);

            Console.WriteLine("==================================");
            Console.WriteLine("====== Sending Student Info ======");
            Console.WriteLine("==================================");

            for (int i = 0; i < 25; i++)
            {
                // Create a new new student to send to the queue
                StudentInfo.StudentInfo newStudent = new StudentInfo.StudentInfo(1111, "Bob Smith", 
                "222-333-1111", "bsmith@yahoo.com", "215-777-8888", "123 Tulip Road, Ambler, PA 19002", 
                "321 Maple Avenue, Lion Town, PA 16800", "John Smith (215-222-6666)", 206, "1111-206", 1);

                string messageBody = JsonConvert.SerializeObject(newStudent);
                var message = new Message(Encoding.UTF8.GetBytes(messageBody));

                Console.WriteLine($"Sending new student");

                // Send the message to the queue
                await queueClient.SendAsync(message);
            }

            Console.ReadKey();

            await queueClient.CloseAsync();
        }

    }
}
