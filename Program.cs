using Resend;
using TestResend.Components;
using TestResend.Services;

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

            // Register Resend Email Service
            builder.Services.AddTransient<ResendEmailService>();

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
