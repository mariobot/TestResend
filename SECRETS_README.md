# User Secrets Configuration

This project uses .NET User Secrets to store sensitive API keys securely.

## Setup Instructions

### 1. Initialize User Secrets (Already Done)
Your project already has a UserSecretsId: `e1b8ecf3-d9c2-4f67-96c1-9c7977092c74`

### 2. Set Your API Keys

Use the following commands to configure your secrets:

```powershell
# Resend API Key
dotnet user-secrets set "ResendApi:Key" "your_resend_api_key_here"

# Brevo (formerly Sendinblue) API Key
dotnet user-secrets set "BravoApi:Key" "your_brevo_api_key_here"

# Twilio SendGrid API Key
dotnet user-secrets set "TwilioApi:ApiKey" "your_sendgrid_api_key_here"

# Mailchimp/Mandrill API Key
dotnet user-secrets set "MailchimpApi:ApiKey" "your_mailchimp_api_key_here"
```

### 3. View Current Secrets

```powershell
dotnet user-secrets list
```

### 4. Clear a Secret

```powershell
dotnet user-secrets remove "ResendApi:Key"
```

### 5. Clear All Secrets

```powershell
dotnet user-secrets clear
```

## Secrets Location

Secrets are stored at:
- **Windows**: `%APPDATA%\Microsoft\UserSecrets\e1b8ecf3-d9c2-4f67-96c1-9c7977092c74\secrets.json`
- **macOS/Linux**: `~/.microsoft/usersecrets/e1b8ecf3-d9c2-4f67-96c1-9c7977092c74/secrets.json`

## API Key Sources

### Resend
- Sign up at: https://resend.com
- Get API key from: https://resend.com/api-keys

### Brevo (formerly Sendinblue)
- Sign up at: https://www.brevo.com
- Get API key from: Settings → SMTP & API → API Keys

### Twilio SendGrid
- Sign up at: https://sendgrid.com
- Get API key from: Settings → API Keys

### Mailchimp/Mandrill
- Sign up at: https://mailchimp.com/developer/transactional/
- Get API key from: Mandrill settings

## Template File

See `secrets.template.json` for the structure of the secrets file (do not commit actual keys to version control).
