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

    public class EmailService : IEmailService
    {
        private readonly string _hostname;
        private readonly string _queueName;
        private readonly IConnectionFactory _connectionFactory;
        private readonly string _smtpServer;
        private readonly int _smtpPort;
        private readonly string _smtpUser;
        private readonly string _smtpPass;

        public EmailService(string hostname, string queueName, string smtpServer, int smtpPort, string smtpUser, string smtpPass)
        {
            _hostname = hostname;
            _queueName = queueName;
            _connectionFactory = new ConnectionFactory() { HostName = _hostname };
            _smtpServer = smtpServer;
            _smtpPort = smtpPort;
            _smtpUser = smtpUser;
            _smtpPass = smtpPass;
        }

        public async Task SendActivationEmailAsync(string token, string email)
        {
            // Sends message to RabbitMQ
            using var connection = _connectionFactory.CreateConnection();
            using var channel = connection.CreateModel();
            channel.QueueDeclare(queue: _queueName,
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            var message = new
            {
                Token = token,
                Email = email,
                ActivationLink = $"https://localhost/activation/{token}"
            };

            var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message));

            channel.BasicPublish(exchange: "",
                                 routingKey: _queueName,
                                 basicProperties: null,
                                 body: body);

            Console.WriteLine("Message sent to RabbitMQ: An activation email to {0} is queued.", email);

            // Sends email using smtp
            await SendEmailViaSmtpAsync(email, "Activation Email", $"Please activate your account using the following link: {message.ActivationLink}");
        }

        private async Task SendEmailViaSmtpAsync(string toEmail, string subject, string body)
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
                Console.WriteLine("Activation email sent to {0}.", toEmail);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to send email to {0}. Exception: {1}", toEmail, ex.Message);
            }
        }
    }
}
