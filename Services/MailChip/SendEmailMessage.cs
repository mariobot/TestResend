using Mandrill.Models;
using Mandrill.Requests.Messages;

namespace TestResend.Services.MailChip
{
    internal class SendEmailMessage : SendMessageRequest
    {
        public SendEmailMessage() : base(new EmailMessage())
        {
        }

        public string FromEmail { get; set; } = string.Empty;
        public List<EmailAddress> To { get; set; } = new();
        public string Subject { get; set; } = string.Empty;
        public string Html { get; set; } = string.Empty;
    }
}
