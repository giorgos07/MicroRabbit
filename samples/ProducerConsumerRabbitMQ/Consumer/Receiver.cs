using System;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Consumer
{
    public static class Receiver
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
                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += ConsumerReceived;
                channel.BasicConsume(queue: queueName, autoAck: true, consumer);
            }
            Console.WriteLine("Please press [enter] to exit.");
            Console.ReadKey();
        }

        private static void ConsumerReceived(object sender, BasicDeliverEventArgs eventArgs) {
            var body = eventArgs.Body;
            var message = Encoding.UTF8.GetString(body.ToArray());
            Console.WriteLine($"Received message: '{message}'");
        }
    }
}
