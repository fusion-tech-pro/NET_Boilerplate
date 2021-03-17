namespace Boilerplate.Services
{
    #region << Using >>

    using System;
    using System.Collections.Generic;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Boilerplate.Domain;
    using Boilerplate.Models;
    using JetBrains.Annotations;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.IdentityModel.Tokens;

    #endregion

    [Injectable(ServiceLifetime.Scoped)]
    [UsedImplicitly]
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

        public async Task<bool> Register(SignUpDto signUpDto)
        {
            var user = new IdentityUser

                       {
                               Email = signUpDto.Email,
                               UserName = signUpDto.Email
                       };

            var result = await this._userManager.CreateAsync(user, signUpDto.Password);

            if (!result.Succeeded)
                return false;

            await this._signInManager.SignInAsync(user, false);
            return true;
        }

        public async Task<bool> SignIn(SignInDto signInDto)
        {
            var user = await this._userManager.FindByEmailAsync(signInDto.Email);

            if (user == null)
                return false;

            var result = await this._signInManager.CheckPasswordSignInAsync(user, signInDto.Password, true);

            if (!result.Succeeded)
                return false;

            await this._signInManager.SignInAsync(user, false);
            return true;
        }

        public async Task<UserDto> GetUserByEmail(string email)
        {
            var user = await this._userManager.FindByEmailAsync(email);

            if (user == null)
                throw new ArgumentNullException();

            var userDto = new UserDto
                          {
                                  Email = user.Email,
                                  Id = user.Id
                          };

            return userDto;
        }

        public object GenerateToken(UserDto user)
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

            var response = new
                           {
                                   encodedJwt,
                                   expiredDate
                           };

            return response;
        }

        #endregion

        private ClaimsIdentity GetIdentity(UserDto user)
        {
            var claims = new List<Claim>
                         {
                                 new Claim("email", user.Email),
                                 new Claim("id", user.Id)
                         };

            var claimsIdentity = new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                                                    ClaimsIdentity.DefaultRoleClaimType);

            return claimsIdentity;
        }
    }
}