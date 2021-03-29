namespace Boilerplate.Utilities
{
    public interface IEmailModel
    {
        #region Properties

        string UserName { get; set; }

        string MailTo { get; set; }

        string Subject { get; set; }

        #endregion
    }
}