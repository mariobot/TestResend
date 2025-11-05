using System.Reflection.Metadata;
using sib_api_v3_sdk.Api;
using sib_api_v3_sdk.Client;
using sib_api_v3_sdk.Model;

namespace TestResend.Services.Bravo
{
    public class BravoEmailService
    {
        private readonly IConfiguration _configuration;
        public BravoEmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async System.Threading.Tasks.Task SendEmailAsync(string from, string to, string subject, string body)
        {
            var apiKey = _configuration["BravoApi:Key"];
            if (string.IsNullOrWhiteSpace(apiKey))
                throw new InvalidOperationException("Bravo API key is missing.");

            Configuration.Default.ApiKey.Add("api-key", apiKey);

            var apiInstance = new TransactionalEmailsApi();

            var sendSmtpEmail = new SendSmtpEmail(
                sender: new SendSmtpEmailSender(email: from),
                to: new List<SendSmtpEmailTo> { new SendSmtpEmailTo(email: to) },
                subject: subject,
                htmlContent: body
            );

            await apiInstance.SendTransacEmailAsync(sendSmtpEmail);

            return;
        }
    }
}
