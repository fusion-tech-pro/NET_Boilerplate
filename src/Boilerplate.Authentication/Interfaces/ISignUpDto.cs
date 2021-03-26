namespace Boilerplate.Authentication
{
    public interface ISignUpDto
    {
        #region Properties

        public string Email { get; set; }

        public string Password { get; set; }

        public string UserName { get; set; }

        #endregion
    }
}