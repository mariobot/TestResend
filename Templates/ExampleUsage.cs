using Resend;
using TestResend.Services;

namespace TestResend.Templates;

/// <summary>
/// Example of how to use the professional email template with Resend
/// </summary>
public class ExampleUsage
{
    private readonly IResend _resend;
    private readonly EmailTemplateService _templateService;

    public ExampleUsage(IResend resend)
    {
        _resend = resend;
        _templateService = new EmailTemplateService();
    }

    /// <summary>
    /// Example 1: Order Confirmation Email
    /// </summary>
    public async Task SendOrderConfirmationAsync(string recipientEmail, string customerName, string orderId)
    {
        // Load the template
        var template = await _templateService.LoadTemplateAsync("ProfessionalEmail");

        // Populate with order data
        var templateData = new EmailTemplateData
        {
            EmailTitle = "Order Confirmation",
            CompanyName = "Your Store",
            CompanyTagline = "Quality Products Delivered",
            
            CustomerName = customerName,
            MainMessage = "Thank you for your order! We're excited to confirm that we've received your order and it's being processed.",
            
            HighlightLabel = "Order Number",
            HighlightValue = $"#{orderId}",
            
            Field1Label = "Order Date",
            Field1Value = DateTime.Now.ToString("MMMM dd, yyyy"),
            Field2Label = "Estimated Delivery",
            Field2Value = DateTime.Now.AddDays(5).ToString("MMMM dd, yyyy"),
            Field3Label = "Total Amount",
            Field3Value = "$125.99",
            Field4Label = "Payment Method",
            Field4Value = "Credit Card •••• 4242",
            
            ButtonText = "Track Your Order",
            ButtonLink = $"https://yourstore.com/orders/{orderId}",
            
            SecondaryMessage = "If you have any questions about your order, feel free to reply to this email or contact our customer support team.",
            
            CompanyAddress = "123 Commerce Street, San Francisco, CA 94102",
            CompanyPhone = "+1 (555) 123-4567",
            CompanyEmail = "support@yourstore.com",
            WebsiteUrl = "https://yourstore.com",
            FacebookUrl = "https://facebook.com/yourstore",
            TwitterUrl = "https://twitter.com/yourstore",
            LinkedInUrl = "https://linkedin.com/company/yourstore",
            DisclaimerText = "You received this email because you placed an order with us.",
            UnsubscribeLink = "https://yourstore.com/unsubscribe"
        };

        var html = _templateService.PopulateTemplate(template, templateData);

        // Send via Resend
        var message = new EmailMessage
        {
            From = "orders@yourstore.com",
            Subject = $"Order Confirmation - #{orderId}",
            HtmlBody = html
        };
        message.To.Add(recipientEmail);

        await _resend.EmailSendAsync(message);
    }

    /// <summary>
    /// Example 2: Welcome Email
    /// </summary>
    public async Task SendWelcomeEmailAsync(string recipientEmail, string userName)
    {
        var template = await _templateService.LoadTemplateAsync("ProfessionalEmail");

        var templateData = new EmailTemplateData
        {
            EmailTitle = "Welcome!",
            CompanyName = "YourApp",
            CompanyTagline = "Simplifying Your Workflow",
            
            CustomerName = userName,
            MainMessage = "Welcome to YourApp! We're thrilled to have you on board. Your account has been successfully created and you're ready to get started.",
            
            HighlightLabel = "Account Created",
            HighlightValue = DateTime.Now.ToString("MMM dd, yyyy"),
            
            Field1Label = "Username",
            Field1Value = userName,
            Field2Label = "Account Type",
            Field2Value = "Free Plan",
            Field3Label = "Storage",
            Field3Value = "5 GB",
            Field4Label = "Team Members",
            Field4Value = "Up to 3",
            
            ButtonText = "Get Started Now",
            ButtonLink = "https://yourapp.com/dashboard",
            
            SecondaryMessage = "Need help getting started? Check out our getting started guide or reach out to our support team - we're here to help!",
            
            CompanyAddress = "456 Tech Lane, Austin, TX 78701",
            CompanyPhone = "+1 (555) 987-6543",
            CompanyEmail = "hello@yourapp.com",
            WebsiteUrl = "https://yourapp.com",
            FacebookUrl = "https://facebook.com/yourapp",
            TwitterUrl = "https://twitter.com/yourapp",
            LinkedInUrl = "https://linkedin.com/company/yourapp"
        };

        var html = _templateService.PopulateTemplate(template, templateData);

        var message = new EmailMessage
        {
            From = "welcome@yourapp.com",
            Subject = "Welcome to YourApp!",
            HtmlBody = html
        };
        message.To.Add(recipientEmail);

        await _resend.EmailSendAsync(message);
    }

    /// <summary>
    /// Example 3: Invoice Email
    /// </summary>
    public async Task SendInvoiceEmailAsync(string recipientEmail, string customerName, string invoiceNumber)
    {
        var template = await _templateService.LoadTemplateAsync("ProfessionalEmail");

        var templateData = new EmailTemplateData
        {
            EmailTitle = "Invoice",
            CompanyName = "Your Business",
            CompanyTagline = "Professional Services",
            
            CustomerName = customerName,
            MainMessage = "Thank you for your business! Please find the details of your invoice below.",
            
            HighlightLabel = "Total Due",
            HighlightValue = "$2,450.00",
            
            Field1Label = "Invoice Number",
            Field1Value = invoiceNumber,
            Field2Label = "Invoice Date",
            Field2Value = DateTime.Now.ToString("MMMM dd, yyyy"),
            Field3Label = "Due Date",
            Field3Value = DateTime.Now.AddDays(30).ToString("MMMM dd, yyyy"),
            Field4Label = "Payment Terms",
            Field4Value = "Net 30",
            
            ButtonText = "View & Pay Invoice",
            ButtonLink = $"https://yourbusiness.com/invoices/{invoiceNumber}",
            
            SecondaryMessage = "Please make payment by the due date. If you have any questions about this invoice, don't hesitate to contact us.",
            
            CompanyAddress = "789 Business Blvd, New York, NY 10001",
            CompanyPhone = "+1 (555) 456-7890",
            CompanyEmail = "billing@yourbusiness.com",
            WebsiteUrl = "https://yourbusiness.com"
        };

        var html = _templateService.PopulateTemplate(template, templateData);

        var message = new EmailMessage
        {
            From = "billing@yourbusiness.com",
            Subject = $"Invoice {invoiceNumber} from Your Business",
            HtmlBody = html
        };
        message.To.Add(recipientEmail);

        await _resend.EmailSendAsync(message);
    }
}