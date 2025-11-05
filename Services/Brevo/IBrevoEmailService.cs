using sib_api_v3_sdk.Model;

namespace TestResend.Services.Bravo
{
    public interface IBrevoEmailService
    {
        Task<CreateSmtpEmail> SendEmailAsync(string from, string to, string subject, string body);
        Task<CreateSmtpEmail> SendEmailWithAttachmentAsync(string from, string to, string subject, string body, byte[] attachmentContent, string attachmentName);
        Task<CreateSmtpEmail> SendBulkEmailAsync(string from, List<string> toEmails, string subject, string body);
    }
}
