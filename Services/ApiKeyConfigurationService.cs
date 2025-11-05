using Microsoft.Extensions.Options;
using Resend;

namespace TestResend.Services
{
    public class ApiKeyConfigurationService
    {
        private readonly IConfiguration _configuration;
        private readonly IOptionsMonitor<ResendClientOptions> _resendOptions;
        private string _resendApiKey;
        private string _bravoApiKey;
        private string _twilioApiKey;
        private string _mailchimpApiKey;

        public ApiKeyConfigurationService(
            IConfiguration configuration,
            IOptionsMonitor<ResendClientOptions> resendOptions)
        {
            _configuration = configuration;
            _resendOptions = resendOptions;
            
            // Load initial values from configuration
            _resendApiKey = configuration["ResendApi:Key"] ?? string.Empty;
            _bravoApiKey = configuration["BravoApi:Key"] ?? string.Empty;
            _twilioApiKey = configuration["TwilioApi:ApiKey"] ?? string.Empty;
            _mailchimpApiKey = configuration["MailchimpApi:ApiKey"] ?? string.Empty;
        }

        public string GetResendApiKey() => _resendApiKey;
        public string GetBravoApiKey() => _bravoApiKey;
        public string GetTwilioApiKey() => _twilioApiKey;
        public string GetMailchimpApiKey() => _mailchimpApiKey;

        public void UpdateResendApiKey(string apiKey)
        {
            _resendApiKey = apiKey;
            _configuration["ResendApi:Key"] = apiKey;
        }

        public void UpdateBravoApiKey(string apiKey)
        {
            _bravoApiKey = apiKey;
            _configuration["BravoApi:Key"] = apiKey;
        }

        public void UpdateTwilioApiKey(string apiKey)
        {
            _twilioApiKey = apiKey;
            _configuration["TwilioApi:ApiKey"] = apiKey;
        }

        public void UpdateMailchimpApiKey(string apiKey)
        {
            _mailchimpApiKey = apiKey;
            _configuration["MailchimpApi:ApiKey"] = apiKey;
        }

        public bool HasAllKeysConfigured()
        {
            return !string.IsNullOrEmpty(_resendApiKey) &&
                   !string.IsNullOrEmpty(_bravoApiKey) &&
                   !string.IsNullOrEmpty(_twilioApiKey) &&
                   !string.IsNullOrEmpty(_mailchimpApiKey);
        }

        public Dictionary<string, bool> GetKeyStatus()
        {
            return new Dictionary<string, bool>
            {
                { "Resend", !string.IsNullOrEmpty(_resendApiKey) },
                { "Bravo", !string.IsNullOrEmpty(_bravoApiKey) },
                { "Twilio", !string.IsNullOrEmpty(_twilioApiKey) },
                { "Mailchimp", !string.IsNullOrEmpty(_mailchimpApiKey) }
            };
        }
    }
}
