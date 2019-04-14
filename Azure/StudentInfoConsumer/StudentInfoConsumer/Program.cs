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
            ReceiveStudentInfo().GetAwaiter().GetResult();
        }

        static async Task ReceiveStudentInfo()
        {
            queueClient = new QueueClient(connectionString, queueName);

            Console.WriteLine("================================================");
            Console.WriteLine("====== Listening for studentInfo Messages ======");
            Console.WriteLine("================================================");

            var messageHandlerOptions = new MessageHandlerOptions(ExceptionReceivedHandler)
            {
                MaxConcurrentCalls = 1,
                AutoComplete = false
            };

            // Register the function that will process messages
            queueClient.RegisterMessageHandler(ProcessMessagesAsync, messageHandlerOptions);


            Console.ReadKey();

            await queueClient.CloseAsync();
        }



        static async Task ProcessMessagesAsync(Message message, CancellationToken token)
        {
            Random random = new Random();
            int DeadLetter = random.Next(1, 3);

            if (DeadLetter == 1)
            {
                Console.WriteLine($"Purposefully Dead-Lettering.");
                await queueClient.AbandonAsync(message.SystemProperties.LockToken);
            }
            else
            {
                // Process the message
                StudentInfo.StudentInfo newStudent = JsonConvert.DeserializeObject<StudentInfo.StudentInfo>(Encoding.UTF8.GetString(message.Body));

                Console.WriteLine($"New Student Recieved: {newStudent.Name}");

                await queueClient.CompleteAsync(message.SystemProperties.LockToken);
            }
        }

        static Task ExceptionReceivedHandler(ExceptionReceivedEventArgs exceptionReceivedEventArgs)
        {
            Console.WriteLine($"Message handler encountered an exception {exceptionReceivedEventArgs.Exception}.");
            var context = exceptionReceivedEventArgs.ExceptionReceivedContext;
            Console.WriteLine("Exception context for troubleshooting:");
            Console.WriteLine($"- Endpoint: {context.Endpoint}");
            Console.WriteLine($"- Entity Path: {context.EntityPath}");
            Console.WriteLine($"- Executing Action: {context.Action}");
            return Task.CompletedTask;
        }
    }
}