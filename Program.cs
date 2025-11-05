using Resend;
using TestResend.Bridge;
using TestResend.Components;
using TestResend.Services;
using TestResend.Services.Bravo;
using TestResend.Services.MailChip;
using TestResend.Services.Resend;
using TestResend.Services.Twilio;

namespace TestResend
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add configuration from user secrets
            builder.Configuration.AddUserSecrets<Program>();

            // Add services to the container.
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();

            string apiKey = builder.Configuration["ResendApi:Key"] ?? string.Empty;
            string apiBravoKey = builder.Configuration["BravoApi:Key"] ?? string.Empty;

            //Register Resend Client
            builder.Services.AddOptions();
            builder.Services.AddHttpClient<ResendClient>();
            builder.Services.Configure<ResendClientOptions>(o =>
            {
                o.ApiToken = apiKey;
            });
            builder.Services.AddTransient<IResend, ResendClient>();

            // Register Email Services
            builder.Services.AddTransient<ResendEmailService>();
            builder.Services.AddTransient<BrevoEmailService>();
            builder.Services.AddTransient<EmailTemplateService>();
            
            // Register Resend Email Service implementations
            builder.Services.AddTransient<IResendEmailService, ResendEmailServiceV3>();
            builder.Services.AddTransient<ResendEmailServiceV3>();
            builder.Services.AddTransient<ResendEmailServiceV2>();
            builder.Services.AddTransient<ResendEmailServiceFactory>();
            builder.Services.AddTransient<IResendEmailServiceV4, ResendEmailServiceV4>();

            // Register Brevo Email Service implementations
            builder.Services.AddTransient<IBrevoEmailService, BrevoEmailServiceV3>();
            builder.Services.AddTransient<BrevoEmailServiceV2>();
            builder.Services.AddTransient<BrevoEmailServiceFactory>();
            builder.Services.AddTransient<BravoEmailServiceV4>();

            // Register MailChip Email Service
            builder.Services.AddTransient<MailChipEmailService>();

            // Requister Twilio SendGrid Email Service
            builder.Services.AddTransient<TwilitoEmailService>();

            // Register API Key Configuration Service
            builder.Services.AddSingleton<ApiKeyConfigurationService>();

            // Register the Bridge
            // Register BridgeApp
            builder.Services.AddTransient<BridgeApp>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseAntiforgery();

            app.MapStaticAssets();
            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();

            app.Run();
        }
    }
}
