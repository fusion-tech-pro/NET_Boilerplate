namespace Boilerplate.Models
{
    #region << Using >>

    using System;
    using FluentValidation;
    using JetBrains.Annotations;

    #endregion

    public class ItemDto
    {
        #region Properties

        public int? Id { get; set; }

        public string Value { get; set; }

        public Status Status { get; set; }

        public DateTime? CreateDate { get; set; }

        public DateTime? UpdateDate { get; set; }

        #endregion

        #region Nested Classes

        [UsedImplicitly]
        public class Validator : AbstractValidator<ItemDto>
        {
            #region Constructors

            public Validator()
            {
                RuleFor(x => x.Value)
                        .NotEmpty().WithMessage("{PropertyName} is empty")
                        .Length(2, 50).WithMessage("Length ({TotalLength} of {PropertyName} invalid)");

                RuleFor(x => x.Status)
                        .NotNull().WithMessage("{PropertyName} is null")
                        .NotEmpty().WithMessage("{PropertyName} is empty");
            }

            #endregion
        }

        #endregion
    }
}