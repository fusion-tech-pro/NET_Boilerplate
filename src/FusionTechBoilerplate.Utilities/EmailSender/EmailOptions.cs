namespace FusionTechBoilerplate.Utilities
{
    public class EmailOptions
    {
        #region Constants

        public const string SettingsSectionKey = "Smtp";

        #endregion

        #region Properties

        public string User { get; set; }

        public string Password { get; set; }

        public string Server { get; set; }

        public int Port { get; set; }

        public bool UseSsl { get; set; }

        public bool RequiresAuthentication { get; set; }

        #endregion
    }
}