namespace FusionTechBoilerplate.Utilities
{
    #region << Using >>

    using System.Threading.Tasks;
    using FluentEmail.Core.Models;
    using Microsoft.AspNetCore.Identity.UI.Services;

    #endregion

    public interface IEmailService : IEmailSender
    {
        Task<SendResponse> SendEmailWithTemplateAsync<T>(string mailTo, string subject, T model);
    }
}