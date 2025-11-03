using Resend;

namespace TestResend.Services
{
    /// <summary>
    /// Alternative implementation with improved structure and return values
    /// </summary>
    public class ResendEmailServiceV2
    {
        private readonly IResend _resend;

        public ResendEmailServiceV2(IResend resend)
        {
            _resend = resend;
        }

        public async Task<EmailSendResponse> SendEmailAsync(string from, string to, string subject, string htmlBody, string? textBody = null)
        {
            var message = new EmailMessage
            {
                From = from,
                Subject = subject,
                HtmlBody = htmlBody,
                TextBody = textBody
            };
            message.To.Add(to);

            return await _resend.EmailSendAsync(message);
        }

        public async Task<EmailSendResponse> SendEmailWithNameAsync(
            string fromEmail,
            string fromName,
            string toEmail,
            string toName,
            string subject,
            string htmlBody,
            string? textBody = null)
        {
            var message = new EmailMessage
            {
                From = $"{fromName} <{fromEmail}>",
                Subject = subject,
                HtmlBody = htmlBody,
                TextBody = textBody
            };
            message.To.Add($"{toName} <{toEmail}>");

            return await _resend.EmailSendAsync(message);
        }
    }
}
