using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace TestResend.Services.Brevo
{
    public class BrevoSmsServise
    {
        private readonly string _apiKey;
        private readonly IHttpClientFactory _httpClientFactory;

        public BrevoSmsServise(IConfiguration configuration, IHttpClientFactory httpClientFactory)
        {
            _apiKey = configuration["BravoApi:Key"];
            _httpClientFactory = httpClientFactory;
        }

        // Send single SMS
        public async Task<bool> SendSmsAsync(string sender, string recipient, string text, string type = "transactional")
        {
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Add("api-key", _apiKey);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var payload = new
            {
                sender = sender,
                recipient = recipient,
                content = text,
                type = type
            };

            var json = JsonSerializer.Serialize(payload);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("https://api.brevo.com/v3/transactionalSMS/sms", content);
            return response.IsSuccessStatusCode;
        }

        // Send bulk SMS
        public async Task<bool> SendBulkSmsAsync(string sender, IEnumerable<string> recipients, string text, string type = "transactional")
        {
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Add("api-key", _apiKey);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var payload = new
            {
                sender = sender,
                recipient = string.Join(",", recipients),
                content = text,
                type = type
            };

            var json = JsonSerializer.Serialize(payload);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("https://api.brevo.com/v3/transactionalSMS/sms", content);
            return response.IsSuccessStatusCode;
        }

        // Get SMS status by message ID
        public async Task<string?> GetSmsStatusAsync(string messageId)
        {
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Add("api-key", _apiKey);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await client.GetAsync($"https://api.brevo.com/v3/transactionalSMS/statistics?messageId={messageId}");
            if (!response.IsSuccessStatusCode)
                return null;

            var json = await response.Content.ReadAsStringAsync();
            return json;
        }
    }
}
