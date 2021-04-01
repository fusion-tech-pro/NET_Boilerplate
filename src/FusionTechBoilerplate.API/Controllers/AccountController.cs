namespace FusionTechBoilerplate.API
{
    #region << Using >>

    using System;
    using System.Threading.Tasks;
    using FusionTechBoilerplate.Authentication;
    using FusionTechBoilerplate.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    #endregion

    [Route("api/auth")]
    [ApiController]
    [AllowAnonymous]
    public class AccountController : ControllerBase
    {
        #region Properties

        private readonly IAuthService _authService;

        #endregion

        #region Constructors

        public AccountController(IAuthService authService)
        {
            this._authService = authService ?? throw new ArgumentNullException(nameof(authService));
        }

        #endregion

        [HttpPost("signin")]
        public async Task<IActionResult> SignIn([FromBody] SignInDto signInDto)
        {
            var response = await this._authService.SignIn(signInDto);
            if (response == null)
                return Unauthorized();

            return Ok(response);
        }

        [HttpPost("signup")]
        public async Task<IActionResult> SignUp([FromBody] SignUpDto signUpDto)
        {
            var response = await this._authService.SignUp(signUpDto);
            if (response == null)
                return Unauthorized();

            return Ok(response);
        }
    }
}