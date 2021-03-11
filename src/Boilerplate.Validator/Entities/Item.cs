namespace Boilerplate.Models
{
    #region << Using >>

    using Boilerplate.Domain;
    using FluentValidation;

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

        #region Nested Classes

        public class Validator : AbstractValidator<Item>
        {
            #region Constructors

            public Validator()
            {
                RuleFor(x => x.Id).NotNull();
                RuleFor(x => x.Value).MinimumLength(2).MaximumLength(100).NotEmpty();
                RuleFor(x => x.Status).NotNull();
            }

            #endregion
        }

        #endregion
    }
}