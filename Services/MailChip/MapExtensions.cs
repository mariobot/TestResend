using Mandrill.Models;
using System.Collections.Generic;

namespace TestResend.Services.MailChip
{
    public static class MapExtensions
    {
internal static SendEmailMessage ToSendEmailMessage(EmailMessage message)
        {
            return new SendEmailMessage
            {
                FromEmail = message.FromEmail,
                To = message.To != null
                    ? new List<EmailAddress>(message.To)
                    : new List<EmailAddress>(),
                Subject = message.Subject,
                Html = message.Html
                // Add other property mappings if needed
            };
        }
    }
}
