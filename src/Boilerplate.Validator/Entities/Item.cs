namespace Boilerplate.Models
{
    #region << Using >>

    using Boilerplate.Domain;

    #endregion

    public class Item : EntityBase
    {
        #region Properties

        public string Value { get; set; }

        public Status Status { get; set; }

        #endregion

        #region Constructors

        public Item()
        {
            Status = Status.New;
        }

        #endregion
    }
}