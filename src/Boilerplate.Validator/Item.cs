namespace Boilerplate.Models
{
    using System;
    using FluentValidation;

    public class Item
    {
        public int Id { get; set; }

        public string Value { get; set; }

        public Status Status { get; set; }

        public DateTime CreateDate { get; private set; }

        public DateTime UpdateDate { get; set; }


        public Item()
        {
            CreateDate = DateTime.UtcNow;
            Status = Status.New;
        }

        public class Validator : AbstractValidator<Item>
        {
            public Validator()
            {
                RuleFor(x => x.Id).NotNull();
                RuleFor(x => x.Value).MinimumLength(2).MaximumLength(100).NotEmpty();
                RuleFor(x => x.Status).NotNull();
            }
        }
    }
}
