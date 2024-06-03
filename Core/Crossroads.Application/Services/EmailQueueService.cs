using Crossroads.Application.Interfaces.Services;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crossroads.Application.Services
{
    using System;
    using RabbitMQ.Client;
    using System.Text;
    using System.Text.Json;
    using System.Net.Mail;
    using System.Net;

    public class EmailQueueService : IEmailQueueService
    {
        private readonly string _hostname;
        private readonly string _queueName;
        private readonly IConnectionFactory _connectionFactory;

        public EmailQueueService(string hostname, string queueName)
        {
            _hostname = hostname;
            _queueName = queueName;
            _connectionFactory = new ConnectionFactory() { HostName = _hostname };
        }

        public async Task QueueMessageAsync(string email, string subject, string body)
        {
            // Sends message to RabbitMQ
            using var connection = _connectionFactory.CreateConnection();
            using var channel = connection.CreateModel();
            channel.QueueDeclare(queue: _queueName,
                                 durable: true,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            var payload = new
            {
                Email = email,
                Subject = subject,
                Body = body
            };

            var messageBody = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(payload));

            channel.BasicPublish(exchange: "",
                                 routingKey: _queueName,
                                 basicProperties: null,
                                 body: messageBody);

            Console.WriteLine("Message sent to RabbitMQ: {0}", JsonSerializer.Serialize(payload));
        }
    }
}
