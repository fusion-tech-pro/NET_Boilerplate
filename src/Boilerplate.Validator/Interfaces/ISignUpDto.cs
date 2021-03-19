namespace Boilerplate.Authentication
{
    #region << Using >>

    #endregion

    #region << Using >>

    #endregion

    public interface ISignUpDto
    {
        #region Properties

        public string Email { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }

        #endregion
    }
}