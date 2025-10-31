# TestResend

TestResend is a simple Blazor project designed to demonstrate how to send emails using different providers.  
It includes implementations for sending emails via Resend and Brevo (Sendinblue) APIs.

## Features

- Send emails using multiple providers (Resend, Brevo/Sendinblue)
- Blazor interactive UI for testing email sending
- Secure API key management using .NET user secrets

## How It Works

- The project provides forms to send test emails.
- Each provider has its own service class for integration.
- API keys are loaded securely from configuration.

## Getting Started

1. Clone the repository.
2. Add your provider API keys to user secrets:
   - `ResendApi:Key` for Resend
   - `BravoApi:Key` for Brevo/Sendinblue
3. Run the project and use the UI to send test emails.

## Extending

You can easily add more email providers by creating new service classes and integrating them into the Blazor UI.

---

This project is intended for demonstration and testing purposes.