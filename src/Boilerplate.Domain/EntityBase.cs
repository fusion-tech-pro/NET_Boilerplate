namespace Boilerplate.Domain
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
    }
}