using Resend;

namespace TestResend.Services.Resend
{
    public interface IResendEmailService
    {
        Task<ResendResponse> SendEmailAsync(string from, string to, string subject, string body);
        Task<ResendResponse> SendEmailWithAttachmentAsync(string from, string to, string subject, string body, byte[] attachmentContent, string attachmentName);
        Task<ResendResponse> SendBulkEmailAsync(string from, List<string> toEmails, string subject, string body);
    }
}
