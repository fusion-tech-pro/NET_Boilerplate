namespace FusionTechBoilerplate.Utilities
{
    #region << Using >>

    using System.Threading.Tasks;
    using FluentEmail.Core;
    using FluentEmail.Core.Models;

    #endregion

    public class EmailService : IEmailService
    {
        #region Constants

        private const string cshtml = "cshtml";

        #endregion

        #region Properties

        private readonly IFluentEmail _fluentEmail;

        #endregion

        #region Constructors

        public EmailService(IFluentEmail fluentEmail)
        {
            this._fluentEmail = fluentEmail;
        }

        #endregion

        #region Interface Implementations

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            await this._fluentEmail.To(email)
                      .Subject(subject)
                      .Body(htmlMessage, true)
                      .SendAsync();
        }

        public async Task<SendResponse> SendEmailWithTemplateAsync<T>(string mailTo, string subject, T model)
        {
            var path = $"{typeof(T).FullName}.{cshtml}";

            return await this._fluentEmail.To(mailTo)
                             .Subject(subject)
                             .UsingTemplateFromEmbedded(path, model, typeof(T).Assembly, true)
                             .SendAsync();
        }

        #endregion
    }
}