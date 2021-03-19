namespace Boilerplate.Authentication
{
    #region << Using >>

    using System;

    #endregion

    public class JWTCreditd
    {
        #region Properties

        public string EncodedJwt { get; set; }

        public DateTime ExpiredDate { get; set; }

        #endregion
    }
}