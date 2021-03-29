namespace Boilerplate.Models
{
    #region << Using >>

    using Boilerplate.Utilities;

    #endregion

    public class EmailDto : IEmailModel
    {
        #region Properties

        public string UserName { get; set; }

        public string MailTo { get; set; }

        public string Subject { get; set; }

        #endregion
    }
}