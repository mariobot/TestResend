using Resend;
using System.Text;

namespace TestResend.Services.Resend
{
    public class ResendEmailService
    {
        private readonly IResend _resend;

        public ResendEmailService(IResend resend)
        {
            _resend = resend;
        }

        public async Task SendEmail(string to, string from, string subject, string? body = null)
        {
            var message = new EmailMessage();
            message.From = from;
            message.To.Add(to);
            message.Subject = EncodeSubject(subject);
            if (body != null)            
                message.TextBody = body;            
            else
                message.HtmlBody = "<strong>it works the email sended from my App with Resend!</strong>";

            await _resend.EmailSendAsync(message);
        }

        private static string EncodeSubject(string subject)
        {
            // Check if the subject contains non-ASCII characters
            if (subject.All(c => c < 128))
                return subject;

            // Encode using RFC 2047 format: =?charset?encoding?encoded-text?=
            var bytes = Encoding.UTF8.GetBytes(subject);
            var encoded = Convert.ToBase64String(bytes);
            return $"=?utf-8?B?{encoded}?=";
        }
    }
}
