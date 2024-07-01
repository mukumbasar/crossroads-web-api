using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Crossroads.Application.Dtos.Email;
using Crossroads.Application.Interfaces.Services;

namespace Crossroads.Application.Services
{
    public class EmailConsumerService : IEmailConsumerService
    {
        private readonly string _hostname;
        private readonly string _queueName;
        private readonly IConnectionFactory _connectionFactory;
        private readonly string _smtpServer;
        private readonly int _smtpPort;
        private readonly string _smtpUser;
        private readonly string _smtpPass;

        public EmailConsumerService(string hostname, string queueName, string smtpServer, int smtpPort, string smtpUser, string smtpPass)
        {
            _hostname = hostname;
            _queueName = queueName;
            _connectionFactory = new ConnectionFactory() { HostName = _hostname};
            _smtpServer = smtpServer;
            _smtpPort = smtpPort;
            _smtpUser = smtpUser;
            _smtpPass = smtpPass;
        }

        public async Task StartConsumingAsync()
        {
            var connection = _connectionFactory.CreateConnection();
            var channel = connection.CreateModel();
            channel.QueueDeclare(queue: _queueName,
                                 durable: true,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            var consumer = new EventingBasicConsumer(channel);

            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var messageString = Encoding.UTF8.GetString(body);
                var message = JsonSerializer.Deserialize<EmailContext>(messageString);

                SendEmailAsync(message.Email, message.Subject, message.Body).Wait(); //Send the message via smtp

                channel.BasicAck(ea.DeliveryTag, false); // Acknowledge the message
            };

            channel.BasicConsume(queue: _queueName,
                                 autoAck: false,
                                 consumer: consumer);

            Console.WriteLine("Consumer started. Listening for messages...");
        }

        private async Task SendEmailAsync(string toEmail, string subject, string body)
        {
            try
            {
                var smtpClient = new SmtpClient(_smtpServer)
                {
                    Port = _smtpPort,
                    Credentials = new NetworkCredential(_smtpUser, _smtpPass),
                    EnableSsl = true,
                };

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(_smtpUser),
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true,
                };
                mailMessage.To.Add(toEmail);

                await smtpClient.SendMailAsync(mailMessage);
                Console.WriteLine("Email sent to {0}.", toEmail);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to send email to {0}. Exception: {1}", toEmail, ex.Message);
            }
        }
    }
}
