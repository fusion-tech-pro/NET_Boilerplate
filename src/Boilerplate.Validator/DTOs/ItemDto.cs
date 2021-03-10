namespace Boilerplate.Models
{
    #region << Using >>

    using System;

    #endregion

    public class ItemDto
    {
        #region Properties

        public int? Id { get; set; }

        public string Value { get; set; }

        public Status Status { get; set; }

        public DateTime? CreateDate { get; private set; }

        public DateTime? UpdateDate { get; set; }

        #endregion
    }
}