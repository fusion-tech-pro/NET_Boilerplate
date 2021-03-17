namespace Boilerplate.API
{
    #region << Using >>

    using System;
    using System.Threading.Tasks;
    using Boilerplate.Models;
    using Boilerplate.Services;
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
            if (!await this._authService.SignIn(signInDto))
                return Unauthorized();

            var user = await this._authService.GetUserByEmail(signInDto.Email);
            var response = this._authService.GenerateToken(user);

            return Ok(response);
        }

        [HttpPost("signup")]
        public async Task<IActionResult> SignUp([FromBody] SignUpDto signUpDto)
        {
            if (!await this._authService.Register(signUpDto))
                return Unauthorized();

            var user = await this._authService.GetUserByEmail(signUpDto.Email);
            var response = this._authService.GenerateToken(user);

            return Ok(response);
        }
    }
}