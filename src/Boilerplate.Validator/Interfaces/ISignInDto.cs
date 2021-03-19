namespace Boilerplate.Authentication
{
    #region << Using >>

    #endregion

    public interface ISignInDto
    {
        #region Properties

        public string Email { get; set; }

        public string Password { get; set; }

        #endregion
    }
}