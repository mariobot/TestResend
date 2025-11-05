using Resend;

namespace TestResend.Services.Resend
{
    /// <summary>
    /// Full-featured implementation with interface support, logging and additional capabilities
    /// </summary>
    public class ResendEmailServiceV3 : IResendEmailService
    {
        private readonly IResend _resend;
        private readonly ILogger<ResendEmailServiceV3> _logger;

        public ResendEmailServiceV3(IResend resend, ILogger<ResendEmailServiceV3> logger)
        {
            _resend = resend;
            _logger = logger;
            _logger.LogInformation("ResendEmailServiceV3 initialized successfully");
        }

        public async Task<ResendResponse> SendEmailAsync(string from, string to, string subject, string body)
        {
            try
            {
                _logger.LogInformation("Sending email from {From} to {To}", from, to);

                var message = new EmailMessage
                {
                    From = from,
                    Subject = subject,
                    HtmlBody = body
                };
                message.To.Add(to);

                var result = await _resend.EmailSendAsync(message);

                _logger.LogInformation("Email sent successfully. MessageId: {MessageId}", result);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to send email from {From} to {To}", from, to);
                throw;
            }
        }

        public async Task<ResendResponse> SendEmailWithAttachmentAsync(
            string from,
            string to,
            string subject,
            string body,
            byte[] attachmentContent,
            string attachmentName)
        {
            try
            {
                _logger.LogInformation("Sending email with attachment from {From} to {To}", from, to);

                var message = new EmailMessage
                {
                    From = from,
                    Subject = subject,
                    HtmlBody = body
                };
                message.To.Add(to);

                // Add attachment
                message.Attachments.Add(new EmailAttachment
                {
                    Content = Convert.ToBase64String(attachmentContent),
                    Filename = attachmentName
                });

                var result = await _resend.EmailSendAsync(message);

                _logger.LogInformation("Email with attachment sent successfully. MessageId: {MessageId}", result);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to send email with attachment from {From} to {To}", from, to);
                throw;
            }
        }

        public async Task<ResendResponse> SendBulkEmailAsync(string from, List<string> toEmails, string subject, string body)
        {
            try
            {
                _logger.LogInformation("Sending bulk email from {From} to {Count} recipients", from, toEmails.Count);

                var message = new EmailMessage
                {
                    From = from,
                    Subject = subject,
                    HtmlBody = body
                };

                foreach (var email in toEmails)
                {
                    message.To.Add(email);
                }

                var result = await _resend.EmailSendAsync(message);

                _logger.LogInformation("Bulk email sent successfully. MessageId: {MessageId}", result);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to send bulk email from {From}", from);
                throw;
            }
        }
    }
}
