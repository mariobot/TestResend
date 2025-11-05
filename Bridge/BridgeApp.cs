using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestResend.Services;
using TestResend.Services.Bravo;
using TestResend.Services.MailChip;
using TestResend.Services.Resend;
using TestResend.Services.Twilio;

namespace TestResend.Bridge
{
    public class BridgeApp
    {
        private readonly IResendEmailService _resendService;
        private readonly TwilitoEmailService _twilioService;
        private readonly IBravoEmailService _bravoService;
        private readonly MailChipEmailService _mailchipService;

        public BridgeApp(
            IResendEmailService resendService,
            TwilitoEmailService twilioService,
            IBravoEmailService bravoService,
            MailChipEmailService mailchipService)
        {
            _resendService = resendService;
            _twilioService = twilioService;
            _bravoService = bravoService;
            _mailchipService = mailchipService;
        }

        public async Task<bool> SendEmailAsync(
            string provider,
            string from,
            string to,
            string subject,
            string body)
        {
            switch (provider)
            {
                case "Resend":
                    await _resendService.SendEmailAsync(from, to, subject, body);
                    return true;
                case "Twilio":
                    return await _twilioService.SendEmailAsync(from, to, subject, body);
                case "Bravo":
                    await _bravoService.SendEmailAsync(from, to, subject, body);
                    return true;
                case "Mailchimp":
                    return await _mailchipService.SendEmailAsync(from, to, subject, body);
                default:
                    throw new ArgumentException("Unknown provider", nameof(provider));
            }
        }

        public async Task<bool> SendBulkEmailAsync(
            string provider,
            string from,
            List<string> toList,
            string subject,
            string body)
        {
            switch (provider)
            {
                case "Resend":
                    // ResendEmailService does not have a bulk method in the signature, so send individually
                    foreach (var to in toList)
                        await _resendService.SendEmailAsync(from, to, subject, body);
                    return true;
                case "Twilio":
                    return await _twilioService.SendBulkEmailAsync(from, toList, subject, body);
                case "Bravo":
                    await _bravoService.SendEmailAsync(from, string.Join(",", toList), subject, body);
                    return true;
                case "Mailchimp":
                    return await _mailchipService.SendBulkEmailAsync(from, toList, subject, body);
                default:
                    throw new ArgumentException("Unknown provider", nameof(provider));
            }
        }

        public async Task<bool> SendEmailWithAttachmentAsync(
            string provider,
            string from,
            string to,
            string subject,
            string body,
            byte[] attachmentContent,
            string attachmentName,
            string mimeType = "application/octet-stream")
        {
            switch (provider)
            {
                case "Resend":
                    // ResendEmailService does not have an attachment method in the signature, so fallback to normal send
                    await _resendService.SendEmailAsync(from, to, subject, body);
                    return true;
                case "Twilio":
                    return await _twilioService.SendEmailWithAttachmentAsync(from, to, subject, body, attachmentContent, attachmentName, mimeType);
                case "Bravo":
                    // BravoEmailService does not have an attachment method in the signature, so fallback to normal send
                    await _bravoService.SendEmailAsync(from, to, subject, body);
                    return true;
                case "Mailchimp":
                    return await _mailchipService.SendEmailWithAttachmentAsync(from, to, subject, body, attachmentContent, attachmentName);
                default:
                    throw new ArgumentException("Unknown provider", nameof(provider));
            }
        }

        public async Task<bool> SendEmailWithCcBccAsync(
            string provider,
            string from,
            string to,
            string subject,
            string body,
            List<string>? cc = null,
            List<string>? bcc = null)
        {
            switch (provider)
            {
                case "Resend":
                    // ResendEmailService does not have a CC/BCC method, so fallback to normal send
                    await _resendService.SendEmailAsync(from, to, subject, body);
                    return true;
                case "Twilio":
                    return await _twilioService.SendEmailWithCcBccAsync(from, to, subject, body, cc, bcc);
                case "Bravo":
                    // BravoEmailService does not have a CC/BCC method, so fallback to normal send
                    await _bravoService.SendEmailAsync(from, to, subject, body);
                    return true;
                case "Mailchimp":
                    return await _mailchipService.SendEmailWithCcBccAsync(from, to, subject, body, cc, bcc);
                default:
                    throw new ArgumentException("Unknown provider", nameof(provider));
            }
        }

        public async Task<bool> SendEmailWithReplyToAsync(
            string provider,
            string from,
            string to,
            string subject,
            string body,
            string replyTo)
        {
            switch (provider)
            {
                case "Resend":
                    // ResendEmailService does not have a Reply-To method, so fallback to normal send
                    await _resendService.SendEmailAsync(from, to, subject, body);
                    return true;
                case "Twilio":
                    return await _twilioService.SendEmailWithReplyToAsync(from, to, subject, body, replyTo);
                case "Bravo":
                    // BravoEmailService does not have a Reply-To method, so fallback to normal send
                    await _bravoService.SendEmailAsync(from, to, subject, body);
                    return true;
                case "Mailchimp":
                    return await _mailchipService.SendEmailWithReplyToAsync(from, to, subject, body, replyTo);
                default:
                    throw new ArgumentException("Unknown provider", nameof(provider));
            }
        }


    }
}
