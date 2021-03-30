namespace Boilerplate.Authentication
{
    public interface ISignInDto
    {
        #region Properties

        string Email { get; set; }

        string Password { get; set; }

        #endregion
    }
}