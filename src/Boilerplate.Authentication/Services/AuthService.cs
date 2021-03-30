namespace Boilerplate.Authentication
{
    #region << Using >>

    using System;
    using System.Collections.Generic;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.IdentityModel.Tokens;

    #endregion

    public class AuthService : IAuthService
    {
        #region Properties

        private readonly SignInManager<IdentityUser> _signInManager;

        private readonly UserManager<IdentityUser> _userManager;

        #endregion

        #region Constructors

        public AuthService(SignInManager<IdentityUser> signInManager,
                           UserManager<IdentityUser> userManager)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
        }

        #endregion

        #region Interface Implementations

        public async Task<JWTCredits> SignUp<T>(T signUpDto) where T : ISignUpDto
        {
            var user = new IdentityUser

                       {
                               Email = signUpDto.Email,
                               UserName = string.IsNullOrWhiteSpace(signUpDto.UserName) ? signUpDto.Email : signUpDto.UserName
                       };

            var result = await this._userManager.CreateAsync(user, signUpDto.Password);

            if (!result.Succeeded)
                return null;

            await this._signInManager.SignInAsync(user, false);

            return GenerateToken(user);
        }

        public async Task<JWTCredits> SignIn<T>(T signInDto) where T : ISignInDto
        {
            var user = await this._userManager.FindByEmailAsync(signInDto.Email);

            if (user == null)
                return null;

            var result = await this._signInManager.CheckPasswordSignInAsync(user, signInDto.Password, true);

            if (!result.Succeeded)
                return null;

            await this._signInManager.SignInAsync(user, false);

            return GenerateToken(user);
        }

        #endregion

        private JWTCredits GenerateToken(IdentityUser user)
        {
            var identity = GetIdentity(user);
            var now = DateTime.UtcNow;
            var expiredDate = now.Add(TimeSpan.FromMinutes(Token.LIFETIME));

            var jwt = new JwtSecurityToken(
                                           Token.ISSUER,
                                           Token.AUDIENCE,
                                           notBefore: now,
                                           claims: identity.Claims,
                                           expires: expiredDate,
                                           signingCredentials: new SigningCredentials(Token.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return new JWTCredits
                   {
                           EncodedJwt = encodedJwt,
                           ExpiredDate = expiredDate
                   };
        }

        private ClaimsIdentity GetIdentity(IdentityUser user)
        {
            var claims = new List<Claim>
                         {
                                 new Claim("email", user.Email),
                                 new Claim("id", user.Id)
                         };

            return new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                                      ClaimsIdentity.DefaultRoleClaimType);
        }
    }
}