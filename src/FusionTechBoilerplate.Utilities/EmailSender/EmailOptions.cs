namespace FusionTechBoilerplate.Utilities.EmailSender
{
    public class EmailOptions
    {
        public const string SettingsSectionKey = "Smtp";

        public string User { get; set;}
        public string Password { get; set;}
        public string Server { get; set;}
        public int Port { get; set;}
        public bool UseSsl { get; set;}
        public bool RequiresAuthentication { get; set; }
    }
}
