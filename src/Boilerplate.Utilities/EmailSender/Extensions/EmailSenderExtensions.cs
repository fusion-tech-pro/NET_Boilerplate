namespace Boilerplate.Utilities
{
    #region << Using >>

    using FluentEmail.MailKitSmtp;
    using Microsoft.Extensions.DependencyInjection;

    #endregion

    public static class EmailSenderExtensions
    {
        public static IServiceCollection AddEmailSender(this IServiceCollection services)
        {
            services.AddFluentEmail("akartashova.itp@gmail.com")
                    .AddRazorRenderer()
                    .AddMailKitSender(new SmtpClientOptions
                                      {
                                              Server = "smtp.gmail.com",
                                              Port = 465,
                                              Password = "consolelog123",
                                              UseSsl = true,
                                              RequiresAuthentication = true,
                                              User = "akartashova.itp@gmail.com"
                                      });

            return services;
        }
    }
}