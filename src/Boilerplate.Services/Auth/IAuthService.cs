namespace Boilerplate.Services
{
    #region << Using >>

    using System.Threading.Tasks;
    using Boilerplate.Models;

    #endregion

    public interface IAuthService
    {
        Task<bool> Register(SignUpDto signUpDto);

        Task<bool> SignIn(SignInDto signInDto);

        Task<UserDto> GetUserByEmail(string email);

        object GenerateToken(UserDto userDto);
    }
}