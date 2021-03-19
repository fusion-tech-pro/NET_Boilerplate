namespace Boilerplate.Services
{
    #region << Using >>

    using System.Threading.Tasks;
    using Boilerplate.Authentication;
    using Boilerplate.Models;

    #endregion

    public interface IAuthenticationService
    {
        Task<JWTCreditd> Register(SignUpDto signUpDto);

        Task<JWTCreditd> SignIn(SignInDto signInDto);
    }
}