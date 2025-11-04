using System.IO;
using System.Threading.Tasks;

namespace TestResend.Services;

public class EmailTemplateService
{
    public async Task<string> LoadTemplateAsync(string templateName)
    {
        var templatePath = Path.Combine("Templates", $"{templateName}.html");
        return await File.ReadAllTextAsync(templatePath);
    }

    public string PopulateTemplate(string template, EmailTemplateData data)
    {
        return template
            // Header
            .Replace("{{EmailTitle}}", data.EmailTitle)
            .Replace("{{CompanyName}}", data.CompanyName)
            .Replace("{{CompanyTagline}}", data.CompanyTagline)
            
            // Content
            .Replace("{{CustomerName}}", data.CustomerName)
            .Replace("{{MainMessage}}", data.MainMessage)
            
            // Highlight Box
            .Replace("{{HighlightLabel}}", data.HighlightLabel)
            .Replace("{{HighlightValue}}", data.HighlightValue)
            
            // Info Fields
            .Replace("{{Field1Label}}", data.Field1Label)
            .Replace("{{Field1Value}}", data.Field1Value)
            .Replace("{{Field2Label}}", data.Field2Label)
            .Replace("{{Field2Value}}", data.Field2Value)
            .Replace("{{Field3Label}}", data.Field3Label)
            .Replace("{{Field3Value}}", data.Field3Value)
            .Replace("{{Field4Label}}", data.Field4Label)
            .Replace("{{Field4Value}}", data.Field4Value)
            
            // Button
            .Replace("{{ButtonText}}", data.ButtonText)
            .Replace("{{ButtonLink}}", data.ButtonLink)
            
            // Secondary Message
            .Replace("{{SecondaryMessage}}", data.SecondaryMessage)
            
            // Footer
            .Replace("{{CompanyAddress}}", data.CompanyAddress)
            .Replace("{{CompanyPhone}}", data.CompanyPhone)
            .Replace("{{CompanyEmail}}", data.CompanyEmail)
            .Replace("{{WebsiteUrl}}", data.WebsiteUrl)
            .Replace("{{FacebookUrl}}", data.FacebookUrl)
            .Replace("{{TwitterUrl}}", data.TwitterUrl)
            .Replace("{{LinkedInUrl}}", data.LinkedInUrl)
            .Replace("{{DisclaimerText}}", data.DisclaimerText)
            .Replace("{{UnsubscribeLink}}", data.UnsubscribeLink);
    }
}

public class EmailTemplateData
{
    // Header
    public string EmailTitle { get; set; } = "Email Notification";
    public string CompanyName { get; set; } = "Your Company";
    public string CompanyTagline { get; set; } = "Your Tagline Here";
    
    // Content
    public string CustomerName { get; set; } = "Customer";
    public string MainMessage { get; set; } = "";
    
    // Highlight Box
    public string HighlightLabel { get; set; } = "Important";
    public string HighlightValue { get; set; } = "";
    
    // Dynamic Fields
    public string Field1Label { get; set; } = "Field 1";
    public string Field1Value { get; set; } = "";
    public string Field2Label { get; set; } = "Field 2";
    public string Field2Value { get; set; } = "";
    public string Field3Label { get; set; } = "Field 3";
    public string Field3Value { get; set; } = "";
    public string Field4Label { get; set; } = "Field 4";
    public string Field4Value { get; set; } = "";
    
    // Button
    public string ButtonText { get; set; } = "View Details";
    public string ButtonLink { get; set; } = "#";
    
    // Secondary Message
    public string SecondaryMessage { get; set; } = "";
    
    // Footer
    public string CompanyAddress { get; set; } = "123 Main St, City, Country";
    public string CompanyPhone { get; set; } = "+1 234 567 890";
    public string CompanyEmail { get; set; } = "info@company.com";
    public string WebsiteUrl { get; set; } = "https://yourwebsite.com";
    public string FacebookUrl { get; set; } = "#";
    public string TwitterUrl { get; set; } = "#";
    public string LinkedInUrl { get; set; } = "#";
    public string DisclaimerText { get; set; } = "This email was sent to you because you are subscribed to our mailing list.";
    public string UnsubscribeLink { get; set; } = "#";
}