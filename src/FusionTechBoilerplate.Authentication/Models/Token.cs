namespace FusionTechBoilerplate.Authentication
{
    #region << Using >>

    using System.Text;
    using Microsoft.IdentityModel.Tokens;

    #endregion

    public class Token
    {
        #region Constants

        public const string ISSUER = "AuthServer";

        public const string AUDIENCE = "AuthClient";

        private const string KEY = "Boilerplate-fusion-tech-03152021";

        public const int LIFETIME = 360;

        #endregion

        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}