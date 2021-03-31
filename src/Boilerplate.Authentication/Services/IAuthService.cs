namespace Boilerplate.Authentication
{
    #region << Using >>

    using System.Threading.Tasks;

    #endregion

    public interface IAuthService
    {
        Task<JWTCredits> SignUp<T>(T signUpDto) where T : ISignUpDto;

        Task<JWTCredits> SignIn<T>(T signInDto) where T : ISignInDto;
    }
}