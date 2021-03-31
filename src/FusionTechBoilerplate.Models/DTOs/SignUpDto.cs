namespace FusionTechBoilerplate.Models
{
    #region << Using >>

    using FluentValidation;
    using FusionTechBoilerplate.Authentication;
    using FusionTechBoilerplate.Domain;
    using JetBrains.Annotations;

    #endregion

    #region << Using >>

    #endregion

    public class SignUpDto : ISignUpDto
    {
        #region Properties

        public string Email { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }

        public string UserName { get; set; }

        #endregion

        #region Nested Classes

        [UsedImplicitly]
        public class Validator : AbstractValidator<SignUpDto>
        {
            #region Constructors

            public Validator()
            {
                RuleFor(x => x.Email)
                        .NotEmpty().WithMessage($"{Constants.FluentValidationConventions.PropertyName} is empty")
                        .Length(8, 50).WithMessage($"Length ({Constants.FluentValidationConventions.TotalLength} of "
                                                 + $"{Constants.FluentValidationConventions.PropertyName} invalid)");

                RuleFor(x => x.Password)
                        .Equal(x => x.ConfirmPassword).WithMessage($"The {Constants.FluentValidationConventions.PropertyName}s do not match")
                        .NotNull().WithMessage($"{Constants.FluentValidationConventions.PropertyName} is null")
                        .NotEmpty().WithMessage($"{Constants.FluentValidationConventions.PropertyName} is empty")
                        .Length(8, 50).WithMessage($"Length ({Constants.FluentValidationConventions.TotalLength} of "
                                                 + $"{Constants.FluentValidationConventions.PropertyName} invalid)");

                RuleFor(x => x.ConfirmPassword)
                        .NotNull().WithMessage($"{Constants.FluentValidationConventions.PropertyName} is null")
                        .NotEmpty().WithMessage($"{Constants.FluentValidationConventions.PropertyName} is empty");
            }

            #endregion
        }

        #endregion
    }
}