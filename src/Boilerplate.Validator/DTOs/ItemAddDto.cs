namespace Boilerplate.Models
{
    using System;

    public class ItemAddDto
    {
        public string Value { get; set; }

        public Status Status { get; set; }

        public DateTime CreateDate { get; private set; }
    }
}