using System;
using System.Text;
using RabbitMQ.Client;

namespace Producer
{
    public static class Sender
    {
        public static void Main(string[] args) {
            var factory = new ConnectionFactory {
                HostName = "localhost",
                Port = 5672
            };
            const string queueName = "BasicTest";
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel()) {
                channel.QueueDeclare(queue: queueName, durable: false, exclusive: false, autoDelete: true, arguments: null);
                var message = "Getting started with .NET Core RabbitMQ.";
                var messageBody = Encoding.UTF8.GetBytes(message);
                channel.BasicPublish(exchange: string.Empty, routingKey: queueName, basicProperties: null, body: messageBody);
                Console.WriteLine($"Message: '{message}' was sent to queue: '{queueName}'.");
            }
            Console.WriteLine("Please press [enter] to exit.");
            Console.ReadKey();
        }
    }
}
