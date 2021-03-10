namespace Boilerplate.Models
{
    using System;

    public class ItemDto
    {
        public int Id { get; set; }

        public string Value { get; set; }

        public Status Status { get; set; }

        public DateTime CreateDate { get; private set; }

        public DateTime UpdateDate { get; set; }
    }
}
