namespace Boilerplate.Utilities
{
    #region << Using >>

    using FluentEmail.MailKitSmtp;
    using Microsoft.AspNetCore.Identity.UI.Services;
    using Microsoft.Extensions.DependencyInjection;

    #endregion

    public static class EmailSenderExtensions
    {
        public static IServiceCollection AddEmailSender(this IServiceCollection services,
                                                        string userEmail,
                                                        string password,
                                                        string server,
                                                        int port,
                                                        bool useSll,
                                                        bool requiresAuthentication
                )
        {
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IEmailSender, EmailService>();

            services.AddFluentEmail(userEmail)
                    .AddRazorRenderer()
                    .AddMailKitSender(new SmtpClientOptions
                                      {
                                              User = userEmail,
                                              Password = password,
                                              Server = server,
                                              Port = port,
                                              UseSsl = useSll,
                                              RequiresAuthentication = requiresAuthentication
                                      });

            return services;
        }
    }
}