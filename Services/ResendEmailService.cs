using Resend;

namespace TestResend.Services
{
    public class ResendEmailService
    {
        private readonly IResend _resend;

        public ResendEmailService(IResend resend)
        {
            _resend = resend;
        }

        public async Task SendEmail(string to, string from, string subject, string body = null)
        {
            var message = new EmailMessage();
            message.From = from;
            message.To.Add(to);
            message.Subject = subject;
            if (body != null)            
                message.TextBody = body;            
            else
                message.HtmlBody = "<strong>it works the email sended from my App with Resend!</strong>";

            await _resend.EmailSendAsync(message);
        }
    }
}
