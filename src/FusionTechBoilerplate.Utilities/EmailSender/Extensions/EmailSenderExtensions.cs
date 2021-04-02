namespace FusionTechBoilerplate.Utilities
{
    #region << Using >>

    using FluentEmail.MailKitSmtp;
    using FusionTechBoilerplate.Utilities.EmailSender;
    using Microsoft.AspNetCore.Identity.UI.Services;
    using Microsoft.Extensions.DependencyInjection;

    #endregion

    public static class EmailSenderExtensions
    {
        public static IServiceCollection AddEmailSender(this IServiceCollection services,
                                                        EmailOptions emailOptions
                )
        {
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IEmailSender, EmailService>();

            services.AddFluentEmail(emailOptions.User)
                    .AddRazorRenderer()
                    .AddMailKitSender(new SmtpClientOptions
                                      {
                                              User = emailOptions.User,
                                              Password = emailOptions.Password,
                                              Server = emailOptions.Server,
                                              Port = emailOptions.Port,
                                              UseSsl = emailOptions.UseSsl,
                                              RequiresAuthentication = emailOptions.RequiresAuthentication
                                      });

            return services;
        }
    }
}