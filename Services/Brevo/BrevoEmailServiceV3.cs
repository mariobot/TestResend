using sib_api_v3_sdk.Api;
using sib_api_v3_sdk.Client;
using sib_api_v3_sdk.Model;

namespace TestResend.Services.Bravo
{
    /// <summary>
    /// Full-featured implementation with interface support and additional capabilities
    /// </summary>
    public class BrevoEmailServiceV3 : IBrevoEmailService
    {
        private readonly TransactionalEmailsApi _apiInstance;
        private readonly ILogger<BrevoEmailServiceV3> _logger;

        public BrevoEmailServiceV3(IConfiguration configuration, ILogger<BrevoEmailServiceV3> logger)
        {
            _logger = logger;

            var apiKey = configuration["BravoApi:Key"];
            if (string.IsNullOrWhiteSpace(apiKey))
            {
                _logger.LogError("Bravo API key is missing in configuration");
                throw new InvalidOperationException("Bravo API key is missing.");
            }

            Configuration.Default.ApiKey["api-key"] = apiKey;
            _apiInstance = new TransactionalEmailsApi();
            
            _logger.LogInformation("BravoEmailServiceV3 initialized successfully");
        }

        public async Task<CreateSmtpEmail> SendEmailAsync(string from, string to, string subject, string body)
        {
            try
            {
                _logger.LogInformation("Sending email from {From} to {To}", from, to);

                var sendSmtpEmail = new SendSmtpEmail(
                    sender: new SendSmtpEmailSender(email: from),
                    to: new List<SendSmtpEmailTo> { new SendSmtpEmailTo(email: to) },
                    subject: subject,
                    htmlContent: body
                );

                var result = await _apiInstance.SendTransacEmailAsync(sendSmtpEmail);
                
                _logger.LogInformation("Email sent successfully. MessageId: {MessageId}", result.MessageId);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to send email from {From} to {To}", from, to);
                throw;
            }
        }

        public async Task<CreateSmtpEmail> SendEmailWithAttachmentAsync(
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

                var attachment = new SendSmtpEmailAttachment(
                    content: attachmentContent,
                    name: attachmentName
                );

                var sendSmtpEmail = new SendSmtpEmail(
                    sender: new SendSmtpEmailSender(email: from),
                    to: new List<SendSmtpEmailTo> { new SendSmtpEmailTo(email: to) },
                    subject: subject,
                    htmlContent: body,
                    attachment: new List<SendSmtpEmailAttachment> { attachment }
                );

                var result = await _apiInstance.SendTransacEmailAsync(sendSmtpEmail);
                
                _logger.LogInformation("Email with attachment sent successfully. MessageId: {MessageId}", result.MessageId);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to send email with attachment from {From} to {To}", from, to);
                throw;
            }
        }

        public async Task<CreateSmtpEmail> SendBulkEmailAsync(string from, List<string> toEmails, string subject, string body)
        {
            try
            {
                _logger.LogInformation("Sending bulk email from {From} to {Count} recipients", from, toEmails.Count);

                var recipients = toEmails.Select(email => new SendSmtpEmailTo(email: email)).ToList();

                var sendSmtpEmail = new SendSmtpEmail(
                    sender: new SendSmtpEmailSender(email: from),
                    to: recipients,
                    subject: subject,
                    htmlContent: body
                );

                var result = await _apiInstance.SendTransacEmailAsync(sendSmtpEmail);
                
                _logger.LogInformation("Bulk email sent successfully. MessageId: {MessageId}", result.MessageId);
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
