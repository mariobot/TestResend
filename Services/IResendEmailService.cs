using Resend;

namespace TestResend.Services
{
    public interface IResendEmailService
    {
        Task<EmailSendResponse> SendEmailAsync(string from, string to, string subject, string body);
        Task<EmailSendResponse> SendEmailWithAttachmentAsync(string from, string to, string subject, string body, byte[] attachmentContent, string attachmentName);
        Task<EmailSendResponse> SendBulkEmailAsync(string from, List<string> toEmails, string subject, string body);
    }
}
