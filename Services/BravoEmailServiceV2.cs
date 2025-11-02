using sib_api_v3_sdk.Api;
using sib_api_v3_sdk.Client;
using sib_api_v3_sdk.Model;

namespace TestResend.Services
{
    /// <summary>
    /// Alternative implementation of Brevo email service with singleton API instance
    /// </summary>
    public class BravoEmailServiceV2
    {
        private readonly TransactionalEmailsApi _apiInstance;

        public BravoEmailServiceV2(IConfiguration configuration)
        {
            var apiKey = configuration["BravoApi:Key"];
            if (string.IsNullOrWhiteSpace(apiKey))
                throw new InvalidOperationException("Bravo API key is missing.");

            Configuration.Default.ApiKey["api-key"] = apiKey;
            _apiInstance = new TransactionalEmailsApi();
        }

        public async Task<CreateSmtpEmail> SendEmailAsync(string from, string to, string subject, string body)
        {
            var sendSmtpEmail = new SendSmtpEmail(
                sender: new SendSmtpEmailSender(email: from),
                to: new List<SendSmtpEmailTo> { new SendSmtpEmailTo(email: to) },
                subject: subject,
                htmlContent: body
            );

            return await _apiInstance.SendTransacEmailAsync(sendSmtpEmail);
        }

        public async Task<CreateSmtpEmail> SendEmailWithNameAsync(
            string fromEmail, 
            string fromName, 
            string toEmail, 
            string toName, 
            string subject, 
            string htmlBody, 
            string? textBody = null)
        {
            var sendSmtpEmail = new SendSmtpEmail(
                sender: new SendSmtpEmailSender(email: fromEmail, name: fromName),
                to: new List<SendSmtpEmailTo> { new SendSmtpEmailTo(email: toEmail, name: toName) },
                subject: subject,
                htmlContent: htmlBody,
                textContent: textBody
            );

            return await _apiInstance.SendTransacEmailAsync(sendSmtpEmail);
        }
    }
}
