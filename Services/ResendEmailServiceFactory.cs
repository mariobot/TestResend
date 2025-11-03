using Resend;

namespace TestResend.Services
{
    /// <summary>
    /// Factory pattern implementation for creating Resend email instances
    /// </summary>
    public class ResendEmailServiceFactory
    {
        private readonly IResend _resend;

        public ResendEmailServiceFactory(IResend resend)
        {
            _resend = resend;
        }

        public IResend CreateInstance()
        {
            return _resend;
        }
    }

    /// <summary>
    /// Service using the factory pattern with additional features
    /// </summary>
    public class ResendEmailServiceV4
    {
        private readonly ResendEmailServiceFactory _factory;

        public ResendEmailServiceV4(ResendEmailServiceFactory factory)
        {
            _factory = factory;
        }

        public async Task<Resend.ResendResponse> SendEmailAsync(string from, string to, string subject, string body)
        {
            var resend = _factory.CreateInstance();

            var message = new EmailMessage
            {
                From = from,
                Subject = subject,
                HtmlBody = body
            };
            message.To.Add(to);

            return await resend.EmailSendAsync(message);
        }

        public async Task<ResendResponse> SendEmailWithCcBccAsync(
            string from,
            string to,
            string subject,
            string body,
            List<string>? cc = null,
            List<string>? bcc = null)
        {
            var resend = _factory.CreateInstance();

            var message = new EmailMessage
            {
                From = from,
                Subject = subject,
                HtmlBody = body
            };
            message.To.Add(to);

            if (cc != null)
            {
                foreach (var ccEmail in cc)
                {
                    message.Cc.Add(ccEmail);
                }
            }

            if (bcc != null)
            {
                foreach (var bccEmail in bcc)
                {
                    message.Bcc.Add(bccEmail);
                }
            }

            return await resend.EmailSendAsync(message);
        }

        public async Task<ResendResponse> SendEmailWithReplyToAsync(
            string from,
            string to,
            string subject,
            string body,
            string replyTo)
        {
            var resend = _factory.CreateInstance();

            var message = new EmailMessage
            {
                From = from,
                Subject = subject,
                HtmlBody = body,
                ReplyTo = replyTo
            };
            message.To.Add(to);

            return await resend.EmailSendAsync(message);
        }

        public async Task<ResendResponse> SendEmailWithTagsAsync(
            string from,
            string to,
            string subject,
            string body,
            List<Resend.EmailTag> tags)
        {
            var resend = _factory.CreateInstance();

            var message = new EmailMessage
            {
                From = from,
                Subject = subject,
                HtmlBody = body
            };
            message.To.Add(to);

            foreach (var tag in tags)
            {
                message.Tags.Add(tag);
            }

            return await resend.EmailSendAsync(message);
        }
    }
}
