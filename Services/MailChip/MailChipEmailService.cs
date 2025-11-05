using Mandrill;
using Mandrill.Models;
using Microsoft.Extensions.Configuration;

namespace TestResend.Services.MailChip
{
    public class MailChipEmailService
    {
        private readonly string _apiKey;

        public MailChipEmailService(IConfiguration configuration)
        {
            _apiKey = configuration["MailChip:MandrillApiKey"];
        }

        public async Task<bool> SendEmailAsync(string from, string to, string subject, string body)
        {
            var api = new MandrillApi(_apiKey);

            var message = new EmailMessage
            {
                FromEmail = from,
                To = new List<EmailAddress> { new EmailAddress(to) },
                Subject = subject,
                Html = body
            };

            SendEmailMessage sendEmailMessage = MapExtensions.ToSendEmailMessage(message);

            var result = await api.SendMessage(sendEmailMessage);
            var response = result.FirstOrDefault()?.Status;
            return response == EmailResultStatus.Sent;
        }

        public async Task<bool> SendEmailWithCcBccAsync(
            string from,
            string to,
            string subject,
            string body,
            List<string>? cc = null,
            List<string>? bcc = null)
        {
            var api = new MandrillApi(_apiKey);

            var message = new EmailMessage
            {
                FromEmail = from,
                To = new List<EmailAddress> { new EmailAddress(to) },
                Subject = subject,
                Html = body,
            };

            if (cc != null && cc.Count > 0)
            {
                message.Headers["Cc"] = string.Join(",", cc);
            }
            if (bcc != null && bcc.Count > 0)
            {
                message.Headers["Bcc"] = string.Join(",", bcc);
            }

            SendEmailMessage msg = MapExtensions.ToSendEmailMessage(message);

            var result = await api.SendMessage(msg);
            var response = result.FirstOrDefault()?.Status;
            return response == EmailResultStatus.Sent;
        }

        public async Task<bool> SendEmailWithAttachmentAsync(
            string from,
            string to,
            string subject,
            string body,
            byte[] attachmentContent,
            string attachmentName)
        {
            var api = new MandrillApi(_apiKey);

            var message = new SendEmailMessage
            {
                FromEmail = from,
                To = new List<EmailAddress> { new EmailAddress(to) },
                Subject = subject,
                Html = body,
                /*Attachments = new List<Attachment>
                {
                    new Attachment
                    {
                        Name = attachmentName,
                        Content = Convert.ToBase64String(attachmentContent),
                        Type = "application/octet-stream"
                    }
                }*/
            };

            var result = await api.SendMessage(message);
            var response = result.FirstOrDefault()?.Status;
            return response == EmailResultStatus.Sent;
        }

        public async Task<bool> SendBulkEmailAsync(
            string from,
            List<string> toList,
            string subject,
            string body)
        {
            var api = new MandrillApi(_apiKey);

            var message = new EmailMessage
            {
                FromEmail = from,
                To = toList.Select(email => new EmailAddress(email)).ToList(),
                Subject = subject,
                Html = body
            };

            SendEmailMessage msg = MapExtensions.ToSendEmailMessage(message);

            var result = await api.SendMessage(msg);
            return result.All(r => r.Status == EmailResultStatus.Sent);
        }

        public async Task<bool> SendEmailWithReplyToAsync(
            string from,
            string to,
            string subject,
            string body,
            string replyTo)
        {
            var api = new MandrillApi(_apiKey);

            var message = new EmailMessage
            {
                FromEmail = from,
                To = new List<EmailAddress> { new EmailAddress(to) },
                Subject = subject,
                Html = body,
            };

            SendEmailMessage msg = MapExtensions.ToSendEmailMessage(message);

            var result = await api.SendMessage(msg);
            var response = result.FirstOrDefault()?.Status;
            return response == EmailResultStatus.Sent;
        }

        public async Task<bool> SendEmailWithTagsAsync(
            string from,
            string to,
            string subject,
            string body,
            List<string> tags)
        {
            var api = new MandrillApi(_apiKey);

            var message = new EmailMessage
            {
                FromEmail = from,
                To = new List<EmailAddress> { new EmailAddress(to) },
                Subject = subject,
                Html = body,
                Tags = tags
            };

            SendEmailMessage msg = MapExtensions.ToSendEmailMessage(message);

            var result = await api.SendMessage(msg);
            var response = result.FirstOrDefault()?.Status;
            return response == EmailResultStatus.Sent;
        }
    }
}
