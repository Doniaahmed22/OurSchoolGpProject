using System.Net.Mail;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using School.Services.Dtos.EmailSending;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;
using MailKit.Net.Smtp;

namespace School.Services.EmailServices
{

    public class EmailService : IEmailService
    {
            
           private readonly EmailSettings _emailSettings;
           private readonly ILogger<EmailService> _logger;

           public EmailService(IOptions<EmailSettings> emailSettings, ILogger<EmailService> logger)
           {
               _emailSettings = emailSettings.Value;
               _logger = logger;
           }

           public async Task SendEmailAsync(string toEmail, string subject, string body)
           {
               var emailMessage = new MimeMessage();
               emailMessage.From.Add(new MailboxAddress(_emailSettings.SenderName, _emailSettings.SenderEmail));
               emailMessage.To.Add(new MailboxAddress("", toEmail));
               emailMessage.Subject = subject;
               emailMessage.Body = new TextPart("html") { Text = body };

               using var client = new MailKit.Net.Smtp.SmtpClient();
               try
               {
                   _logger.LogInformation("Connecting to SMTP server {SmtpServer} on port {Port}", _emailSettings.SmtpServer, _emailSettings.Port);

                   await client.ConnectAsync(
                       _emailSettings.SmtpServer,
                       _emailSettings.Port,
                       _emailSettings.Port == 465 ? MailKit.Security.SecureSocketOptions.SslOnConnect : MailKit.Security.SecureSocketOptions.StartTls
                   );

                   _logger.LogInformation("Connected to SMTP server.");

                   await client.AuthenticateAsync(_emailSettings.Username, _emailSettings.Password);
                   _logger.LogInformation("Authenticated with SMTP server.");

                   await client.SendAsync(emailMessage);
                   _logger.LogInformation("Email sent successfully to {ToEmail}", toEmail);
               }
               catch (SmtpCommandException ex)
               {
                   _logger.LogError(ex, "Error sending email: {Message}", ex.Message);
                   throw;
               }
               catch (SmtpProtocolException ex)
               {
                   _logger.LogError(ex, "Protocol error while sending email: {Message}", ex.Message);
                   throw;
               }
               catch (Exception ex)
               {
                   _logger.LogError(ex, "Unexpected error while sending email: {Message}", ex.Message);
                   throw;
               }
               finally
               {
                   await client.DisconnectAsync(true);
                   _logger.LogInformation("Disconnected from SMTP server.");
               }
           }

        }
    }


   