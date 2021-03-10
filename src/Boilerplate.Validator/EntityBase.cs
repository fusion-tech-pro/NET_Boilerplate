namespace Boilerplate.Models
{
    #region << Using >>

    using System;

    #endregion

    public class EntityBase
    {
        #region Properties

        public int Id { get; set; }

        public DateTime CreateDate { get; private set; }

        public DateTime UpdateDate { get; set; }

        #endregion

        #region Constructors

        public EntityBase()
        {
            CreateDate = DateTime.UtcNow;
        }

        #endregion
    }
}