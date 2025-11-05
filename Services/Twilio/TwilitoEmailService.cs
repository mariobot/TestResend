using SendGrid;
using SendGrid.Helpers.Mail;
using Microsoft.Extensions.Configuration;

namespace TestResend.Services.Twilio
{
    public class TwilitoEmailService
    {
        private readonly string _apiKey;

        public TwilitoEmailService(IConfiguration configuration)
        {
            _apiKey = configuration["Twilio:SendGridApiKey"];
        }

        public async Task<bool> SendEmailAsync(string from, string to, string subject, string body)
        {
            var client = new SendGridClient(_apiKey);
            var fromEmail = new EmailAddress(from);
            var toEmail = new EmailAddress(to);

            var msg = MailHelper.CreateSingleEmail(
                fromEmail,
                toEmail,
                subject,
                plainTextContent: body,
                htmlContent: body
            );

            var response = await client.SendEmailAsync(msg);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> SendEmailWithCcBccAsync(
            string from,
            string to,
            string subject,
            string body,
            List<string>? cc = null,
            List<string>? bcc = null)
        {
            var client = new SendGridClient(_apiKey);
            var msg = new SendGridMessage
            {
                From = new EmailAddress(from),
                Subject = subject,
                PlainTextContent = body,
                HtmlContent = body
            };
            msg.AddTo(to);

            if (cc != null)
            {
                foreach (var ccEmail in cc)
                    msg.AddCc(ccEmail);
            }
            if (bcc != null)
            {
                foreach (var bccEmail in bcc)
                    msg.AddBcc(bccEmail);
            }

            var response = await client.SendEmailAsync(msg);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> SendEmailWithAttachmentAsync(
            string from,
            string to,
            string subject,
            string body,
            byte[] attachmentContent,
            string attachmentName,
            string mimeType = "application/octet-stream")
        {
            var client = new SendGridClient(_apiKey);
            var msg = new SendGridMessage
            {
                From = new EmailAddress(from),
                Subject = subject,
                PlainTextContent = body,
                HtmlContent = body
            };
            msg.AddTo(to);

            msg.AddAttachment(attachmentName, Convert.ToBase64String(attachmentContent), mimeType);

            var response = await client.SendEmailAsync(msg);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> SendBulkEmailAsync(
            string from,
            List<string> toList,
            string subject,
            string body)
        {
            var client = new SendGridClient(_apiKey);
            var msg = new SendGridMessage
            {
                From = new EmailAddress(from),
                Subject = subject,
                PlainTextContent = body,
                HtmlContent = body
            };

            foreach (var to in toList)
            {
                msg.AddTo(to);
            }

            var response = await client.SendEmailAsync(msg);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> SendEmailWithReplyToAsync(
            string from,
            string to,
            string subject,
            string body,
            string replyTo)
        {
            var client = new SendGridClient(_apiKey);
            var msg = new SendGridMessage
            {
                From = new EmailAddress(from),
                Subject = subject,
                PlainTextContent = body,
                HtmlContent = body,
                ReplyTo = new EmailAddress(replyTo)
            };
            msg.AddTo(to);

            var response = await client.SendEmailAsync(msg);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> SendEmailWithTagsAsync(
            string from,
            string to,
            string subject,
            string body,
            List<string> tags)
        {
            var client = new SendGridClient(_apiKey);
            var msg = new SendGridMessage
            {
                From = new EmailAddress(from),
                Subject = subject,
                PlainTextContent = body,
                HtmlContent = body
            };
            msg.AddTo(to);

            // SendGrid uses categories for tags
            if (tags != null)
            {
                foreach (var tag in tags)
                {
                    msg.Categories.Add(tag);
                }
            }

            var response = await client.SendEmailAsync(msg);
            return response.IsSuccessStatusCode;
        }
    }
}
