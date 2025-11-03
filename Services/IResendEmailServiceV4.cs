using Resend;

namespace TestResend.Services
{
    public interface IResendEmailServiceV4
    {
        Task<ResendResponse> SendEmailAsync(string from, string to, string subject, string body);

        Task<ResendResponse> SendEmailWithCcBccAsync(
            string from,
            string to,
            string subject,
            string body,
            List<string>? cc = null,
            List<string>? bcc = null);

        Task<ResendResponse> SendEmailWithAttachmentAsync(
            string from,
            string to,
            string subject,
            string body,
            byte[] attachmentContent,
            string attachmentName);

        Task<ResendResponse> SendBulkEmailAsync(
            string from,
            List<string> toList,
            string subject,
            string body);

        Task<ResendResponse> SendEmailWithReplyToAsync(
            string from,
            string to,
            string subject,
            string body,
            string replyTo);

        Task<ResendResponse> SendEmailWithTagsAsync(
            string from,
            string to,
            string subject,
            string body,
            List<EmailTag> tags);
    }
}
