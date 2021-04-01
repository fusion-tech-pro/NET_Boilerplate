namespace FusionTechBoilerplate.Domain
{
    public abstract class Constants
    {
        #region Nested Classes

        public abstract class FluentValidationConventions
        {
            #region Constants

            public const string PropertyName = "{PropertyName}";

            public const string TotalLength = "{TotalLength}";

            #endregion
        }

        public abstract class HttpContentType
        {
            #region Constants

            public const string ContentType = "application/json";

            #endregion
        }

        public abstract class FileExtensions
        {
            #region Constants

            public const string cshtml = "cshtml";

            #endregion
        }

        #endregion
    }
}