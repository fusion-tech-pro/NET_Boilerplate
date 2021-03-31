namespace FusionTechBoilerplate.Models
{
    #region << Using >>

    using System;
    using Boilerplate.Domain;
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
                        .NotEmpty().WithMessage($"{Constants.FluentValidationConventions.PropertyName} is empty")
                        .Length(2, 50).WithMessage($"Length ({Constants.FluentValidationConventions.TotalLength} of "
                                                 + $"{Constants.FluentValidationConventions.PropertyName} invalid)");

                RuleFor(x => x.Status)
                        .NotNull().WithMessage($"{Constants.FluentValidationConventions.PropertyName} is null")
                        .NotEmpty().WithMessage($"{Constants.FluentValidationConventions.PropertyName} is empty");
            }

            #endregion
        }

        #endregion
    }
}