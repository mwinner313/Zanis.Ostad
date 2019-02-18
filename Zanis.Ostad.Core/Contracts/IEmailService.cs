using System.Threading.Tasks;

namespace Zanis.Ostad.Core.Contracts
{
    public interface IEmailService
    {
        Task SendAsync(EmailMessage message);
    }
    public class EmailMessage
    {
        public string Destination { get; set; }
        public string Body { get; set; }
        public string Subject { get; set; }
    }
    public class EmailSenderInfo
    {
        public string UserName { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public bool IsSSlEnabled { get; set; }
        public string Password { get; set; }
        public string From { get; set; }
    }
}