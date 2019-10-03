using MailKit.Net.Smtp;
using MechKeyboardBase.Core.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MechKeyboardBase.Web.Services
{
    public class EmailService : IEmailService
    {
        private readonly IHostingEnvironment _env;

        public EmailService(IHostingEnvironment env)
        {
            _env = env;
        }

        public async Task SendEmailAsync(string email, string subject, string message)
        {
            try
            {
                var mimeMessage = new MimeMessage();

                // TODO: Add a proper email for sending the confirmation link from
                mimeMessage.From.Add(new MailboxAddress("MechkeyboardBase", "Email"));

                mimeMessage.To.Add(new MailboxAddress(email));

                mimeMessage.Subject = subject;

                mimeMessage.Body = new TextPart("html")
                {
                    Text = message
                };

                using (var client = new SmtpClient())
                {
                    // For demo-purposes, accept all SSL certificates (in case the server supports STARTTLS)
                    client.ServerCertificateValidationCallback = (s, c, h, e) => true;

                    if (_env.IsDevelopment())
                    {
                        await client.ConnectAsync("smtp.gmail.com", 465, true);
                    }
                    else
                    {
                        await client.ConnectAsync("smtp.gmail.com");
                    }

                    // TODO: Add a proper email for sending the confirmation link from
                    await client.AuthenticateAsync("Email", "Password");

                    await client.SendAsync(mimeMessage);

                    await client.DisconnectAsync(true);
                }

            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }
    }

    }

