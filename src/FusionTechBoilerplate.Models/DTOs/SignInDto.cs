namespace FusionTechBoilerplate.Models
{
    #region << Using >>

    using Boilerplate.Authentication;
    using Boilerplate.Domain;
    using FluentValidation;
    using FusionTechBoilerplate.Domain;
    using JetBrains.Annotations;

    #endregion

    public class SignInDto : ISignInDto
    {
        #region Properties

        public string Email { get; set; }

        public string Password { get; set; }

        #endregion

        #region Nested Classes

        [UsedImplicitly]
        public class Validator : AbstractValidator<SignInDto>
        {
            #region Constructors

            public Validator()
            {
                RuleFor(x => x.Email)
                        .NotEmpty().WithMessage($"{Constants.FluentValidationConventions.PropertyName} is empty")
                        .Length(4, 50).WithMessage($"Length ({Constants.FluentValidationConventions.TotalLength} of "
                                                 + $"{Constants.FluentValidationConventions.PropertyName} invalid)");

                RuleFor(x => x.Password)
                        .NotNull().WithMessage($"{Constants.FluentValidationConventions.PropertyName} is null")
                        .NotEmpty().WithMessage($"{Constants.FluentValidationConventions.PropertyName} is empty")
                        .Length(8, 50).WithMessage($"Length ({Constants.FluentValidationConventions.TotalLength} of "
                                                 + $"{Constants.FluentValidationConventions.PropertyName} invalid)");
            }

            #endregion
        }

        #endregion
    }
}