using System.Collections.Generic;
using System.Threading.Tasks;
using TestResend.Services.Brevo;
// using TestResend.Services.Twilio; // Uncomment if you add Twilio SMS
// using TestResend.Services.MailChip; // Uncomment if you add MailChip SMS

namespace TestResend.Bridge
{
    public class BridgeSmsApp
    {
        private readonly BrevoSmsServise _brevoSmsService;
        // private readonly TwilioSmsService _twilioSmsService;
        // private readonly MailChipSmsService _mailChipSmsService;

        public BridgeSmsApp(
            BrevoSmsServise brevoSmsService
        // , TwilioSmsService twilioSmsService
        // , MailChipSmsService mailChipSmsService
        )
        {
            _brevoSmsService = brevoSmsService;
            // _twilioSmsService = twilioSmsService;
            // _mailChipSmsService = mailChipSmsService;
        }

        public async Task<bool> SendSmsAsync(
            string provider,
            string sender,
            string recipient,
            string text)
        {
            switch (provider)
            {
                case "Brevo":
                    return await _brevoSmsService.SendSmsAsync(sender, recipient, text);
                // case "Twilio":
                //     return await _twilioSmsService.SendSmsAsync(sender, recipient, text);
                // case "MailChip":
                //     return await _mailChipSmsService.SendSmsAsync(sender, recipient, text);
                default:
                    throw new System.ArgumentException("Unknown SMS provider", nameof(provider));
            }
        }

        public async Task<bool> SendBulkSmsAsync(
            string provider,
            string sender,
            IEnumerable<string> recipients,
            string text)
        {
            switch (provider)
            {
                case "Brevo":
                    return await _brevoSmsService.SendBulkSmsAsync(sender, recipients, text);
                // case "Twilio":
                //     return await _twilioSmsService.SendBulkSmsAsync(sender, recipients, text);
                // case "MailChip":
                //     return await _mailChipSmsService.SendBulkSmsAsync(sender, recipients, text);
                default:
                    throw new System.ArgumentException("Unknown SMS provider", nameof(provider));
            }
        }

        public async Task<string?> GetSmsStatusAsync(
            string provider,
            string messageId)
        {
            switch (provider)
            {
                case "Brevo":
                    return await _brevoSmsService.GetSmsStatusAsync(messageId);
                // case "Twilio":
                //     return await _twilioSmsService.GetSmsStatusAsync(messageId);
                // case "MailChip":
                //     return await _mailChipSmsService.GetSmsStatusAsync(messageId);
                default:
                    throw new System.ArgumentException("Unknown SMS provider", nameof(provider));
            }
        }
    }
}
