namespace Boilerplate.API
{
    #region << Using >>

    using System.Reflection;
    using System.Threading.Tasks;
    using Boilerplate.API.Templates;
    using Boilerplate.Utilities.Models;
    using FluentEmail.Core;
    using Microsoft.AspNetCore.Mvc;

    #endregion

    #region << Using >>

    #endregion

    [Route("api/email")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        #region Constants

        private const string template = "Boilerplate.API.Templates.HelloWorldTemplate.cshtml";

        #endregion

        [HttpPost("email-sender")]
        public async Task SendEmailAsync([FromServices] IFluentEmail email, EmailModel emailModel)
        {
            await email.To(emailModel.Email)
                       .Subject("test")
                       .UsingTemplateFromEmbedded(template, new HelloWorldViewModel { Name = emailModel.Name }, GetType().GetTypeInfo().Assembly)
                       .SendAsync();
        }
    }
}