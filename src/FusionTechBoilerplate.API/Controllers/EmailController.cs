namespace FusionTechBoilerplate.API
{
    #region << Using >>

    #region << Using >>

    using System.Threading.Tasks;
    using FluentEmail.Core.Models;
    using FusionTechBoilerplate.API.Templates;
    using FusionTechBoilerplate.Models;
    using FusionTechBoilerplate.Utilities;
    using Microsoft.AspNetCore.Mvc;

    #endregion

    #region << Using >>

    #endregion

    #endregion

    [Route("api/email")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        #region Properties

        private readonly IEmailService _emailService;

        #endregion

        #region Constructors

        public EmailController(IEmailService emailService)
        {
            this._emailService = emailService;
        }

        #endregion

        [HttpPost("email-sender")]
        public async Task<SendResponse> SendEmailAsync(EmailDto emailDto)
        {
            var model = new HelloWorldTemplate { Name = emailDto.UserName };

            return await this._emailService.SendEmailWithTemplateAsync(emailDto.MailTo, emailDto.Subject, model);
        }
    }
}