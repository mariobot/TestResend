using Mandrill.Models;
using Mandrill.Requests.Messages;

namespace TestResend.Services.MailChip
{
    internal class SendEmailMessage : SendMessageRequest
    {
        public string FromEmail { get; set; }
        public List<EmailAddress> To { get; set; }
        public string Subject { get; set; }
        public string Html { get; set; }
    }
}