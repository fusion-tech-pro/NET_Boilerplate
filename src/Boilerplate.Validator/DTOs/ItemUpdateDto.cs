namespace Boilerplate.Models
{
    using System;

    public class ItemUpdateDto
    {
        public int Id { get; set; }

        public string Value { get; set; }

        public Status Status { get; set; }
    }
}