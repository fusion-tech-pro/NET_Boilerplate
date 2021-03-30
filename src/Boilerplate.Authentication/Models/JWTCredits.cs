namespace Boilerplate.Authentication
{
    #region << Using >>

    using System;

    #endregion

    public class JWTCredits
    {
        #region Properties

        public string EncodedJwt { get; set; }

        public DateTime ExpiredDate { get; set; }

        #endregion
    }
}