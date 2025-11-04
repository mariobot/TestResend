# Email Templates

This folder contains professional email templates that can be used with the Email Template Builder.

## Available Templates

### 1. Professional Email (ProfessionalEmail.html)
**Style:** Purple Gradient
**Best for:** General purpose, notifications, confirmations
**Features:**
- Modern purple gradient header
- Clean and professional layout
- Information box with multiple fields
- Prominent call-to-action button
- Social media links in footer

### 2. Modern Minimal (ModernMinimal.html)
**Style:** Black & White Minimalist
**Best for:** Premium brands, luxury services, fashion
**Features:**
- Bold black and white design
- Minimalist aesthetic
- Grid layout for information
- Strong typography
- Clean, modern look

### 3. Corporate Blue (CorporateBlue.html)
**Style:** Professional Blue with Gold Accent
**Best for:** Corporate communications, B2B emails, invoices
**Features:**
- Professional blue gradient
- Gold accent stripe
- Table-based information layout
- Dark footer for contrast
- Enterprise-ready design

### 4. Warm Welcome (WarmWelcome.html)
**Style:** Friendly Orange with Warm Tones
**Best for:** Welcome emails, onboarding, friendly communications
**Features:**
- Warm orange gradient
- Emoji support
- Card-based information display
- Rounded elements
- Inviting and friendly design

## Usage

### In the Template Builder Page
1. Navigate to `/template-email-builder`
2. Select your desired template from the dropdown
3. Fill in the form fields
4. Preview the email
5. Send when ready

### Programmatically
```csharp
var templateService = new EmailTemplateService();
var template = await templateService.LoadTemplateAsync("ModernMinimal");

var data = new EmailTemplateData
{
    CompanyName = "Your Company",
    CustomerName = "John Doe",
    MainMessage = "Welcome to our service!",
    // ... other fields
};

var html = templateService.PopulateTemplate(template, data);
```

## Template Variables

All templates support the same set of variables:

### Header
- `{{EmailTitle}}` - Email title (for HTML title tag)
- `{{CompanyName}}` - Your company name
- `{{CompanyTagline}}` - Company tagline or slogan

### Content
- `{{CustomerName}}` - Recipient's name
- `{{MainMessage}}` - Primary message content

### Highlight Box
- `{{HighlightLabel}}` - Label for highlighted information
- `{{HighlightValue}}` - The highlighted value (e.g., order number, amount)

### Information Fields (4 dynamic fields)
- `{{Field1Label}}` / `{{Field1Value}}`
- `{{Field2Label}}` / `{{Field2Value}}`
- `{{Field3Label}}` / `{{Field3Value}}`
- `{{Field4Label}}` / `{{Field4Value}}`

### Call to Action
- `{{ButtonText}}` - Button text
- `{{ButtonLink}}` - Button URL

### Additional Content
- `{{SecondaryMessage}}` - Secondary message or closing remarks

### Footer
- `{{CompanyAddress}}` - Company address
- `{{CompanyPhone}}` - Phone number
- `{{CompanyEmail}}` - Contact email
- `{{WebsiteUrl}}` - Company website
- `{{FacebookUrl}}` - Facebook link
- `{{TwitterUrl}}` - Twitter link
- `{{LinkedInUrl}}` - LinkedIn link
- `{{DisclaimerText}}` - Disclaimer or legal text
- `{{UnsubscribeLink}}` - Unsubscribe URL

## Adding New Templates

To add a new template:

1. Create a new HTML file in the `Templates` folder
2. Use the same variable placeholders (e.g., `{{CompanyName}}`)
3. Add the template option to the dropdown in `TemplateEmailBuilder.razor`
4. Ensure responsive design with `@media` queries

## Email Client Compatibility

All templates are designed with email client compatibility in mind:
- Inline CSS styles
- Table-based layouts where needed
- Mobile-responsive design
- Tested with major email clients

## Customization

You can customize these templates by:
- Modifying the CSS styles in the `<style>` section
- Adjusting colors, fonts, and spacing
- Adding or removing sections
- Changing the layout structure

Remember to test thoroughly after making changes!