using sib_api_v3_sdk.Api;
using sib_api_v3_sdk.Client;
using sib_api_v3_sdk.Model;

namespace TestResend.Services
{
    /// <summary>
    /// Factory pattern implementation for creating Brevo email instances
    /// </summary>
    public class BravoEmailServiceFactory
    {
        private readonly string _apiKey;

        public BravoEmailServiceFactory(IConfiguration configuration)
        {
            _apiKey = configuration["BravoApi:Key"] ?? 
                throw new InvalidOperationException("Bravo API key is missing.");
        }

        public TransactionalEmailsApi CreateApiInstance()
        {
            Configuration.Default.ApiKey["api-key"] = _apiKey;
            return new TransactionalEmailsApi();
        }
    }

    /// <summary>
    /// Service using the factory pattern
    /// </summary>
    public class BravoEmailServiceV4
    {
        private readonly BravoEmailServiceFactory _factory;

        public BravoEmailServiceV4(BravoEmailServiceFactory factory)
        {
            _factory = factory;
        }

        public async Task<CreateSmtpEmail> SendEmailAsync(string from, string to, string subject, string body)
        {
            var apiInstance = _factory.CreateApiInstance();

            var sendSmtpEmail = new SendSmtpEmail(
                sender: new SendSmtpEmailSender(email: from),
                to: new List<SendSmtpEmailTo> { new SendSmtpEmailTo(email: to) },
                subject: subject,
                htmlContent: body
            );

            return await apiInstance.SendTransacEmailAsync(sendSmtpEmail);
        }

        public async Task<CreateSmtpEmail> SendTemplatedEmailAsync(
            string from, 
            string to, 
            long templateId, 
            Dictionary<string, string> parameters)
        {
            var apiInstance = _factory.CreateApiInstance();

            var sendSmtpEmail = new SendSmtpEmail(
                sender: new SendSmtpEmailSender(email: from),
                to: new List<SendSmtpEmailTo> { new SendSmtpEmailTo(email: to) },
                templateId: templateId,
                _params: parameters
            );

            return await apiInstance.SendTransacEmailAsync(sendSmtpEmail);
        }
    }
}
