using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Zanis.Ostad.Core.Contracts;

namespace Zanis.Ostad.Common
{
    
    public class EmailService : IEmailService
    {
        private static bool _initialized;
        private static ConcurrentQueue<EmailMessage> _messages =new ConcurrentQueue<EmailMessage>();
        private static EmailSenderInfo _emailSenderInfo;

        private static void Start()
        {
            if (_initialized) return;
            _initialized = true;
            new Thread(Run){IsBackground = true}.Start();
        }

        private static void Run()
        {
            while (true)
            {
                Thread.Sleep(1000);
                if (!_messages.Any()) continue;
                _messages.TryDequeue(out var message);
                var mail=new MailMessage();
                mail.To.Add(new MailAddress(message.Destination));
                mail.From = new MailAddress(_emailSenderInfo.From);
                mail.Subject = message.Subject;
                mail.Body = message.Body;
                mail.IsBodyHtml = true;
                using (var smtp = new SmtpClient())
                {
                    var credential = new NetworkCredential
                    {
                        UserName = _emailSenderInfo.UserName,  
                        Password = _emailSenderInfo.Password
                    };
                    smtp.Credentials = credential;
                    smtp.Host = _emailSenderInfo.Host;
                    smtp.Port = _emailSenderInfo.Port;
                    smtp.EnableSsl = _emailSenderInfo.IsSSlEnabled;
                    try
                    {
                        smtp.SendMailAsync(mail).Wait();
                    }
                    catch (Exception e)
                    {
                    }
                }
            }
        }

        public EmailService(EmailSenderInfo emailSenderInfo)
        {
            _emailSenderInfo = emailSenderInfo;
        }

        public async Task SendAsync(EmailMessage message)
        {
            Start();
            _messages.Enqueue(message);
        }
    }

   

  
}
