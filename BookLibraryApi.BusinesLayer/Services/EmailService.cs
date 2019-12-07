using BookLibraryApi.BusinesLayer.Intefaces;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BookLibraryApi.BusinesLayer.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;
        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task SendEmailAsync(string email, string subject, string message)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("Administration", "test@gmail.com"));
            emailMessage.To.Add(new MailboxAddress(string.Empty, email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = message
            };
            //set up smtp server for send mail
            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(
                    _configuration["SmtpServer:Server"],
                    Convert.ToInt32(_configuration["SmtpServer:Port"]),
                                          MailKit.Security.SecureSocketOptions.StartTls);
                await client.AuthenticateAsync(_configuration["SmtpServer:SmtpUsername"], _configuration["SmtpServer:SmtpPassword"]);
                await client.SendAsync(emailMessage);

                await client.DisconnectAsync(true);
            }
        }
    }
}
