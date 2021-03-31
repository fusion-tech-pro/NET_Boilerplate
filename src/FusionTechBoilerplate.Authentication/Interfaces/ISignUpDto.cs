namespace FusionTechBoilerplate.Authentication
{
    public interface ISignUpDto
    {
        #region Properties

        string Email { get; set; }

        string Password { get; set; }

        string UserName { get; set; }

        #endregion
    }
}