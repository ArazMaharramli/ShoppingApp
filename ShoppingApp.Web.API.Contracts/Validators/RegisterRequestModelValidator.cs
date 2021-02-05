using FluentValidation;
using ShoppingApp.Web.API.Contracts.RequestModels.V1;

namespace ShoppingApp.Web.API.Contracts.Validators
{
    public class RegisterRequestModelValidator : AbstractValidator<RegisterRequestModel>
    {
        public RegisterRequestModelValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(x => x.FirstName)
                .NotEmpty();

            RuleFor(x => x.LastName)
                .NotEmpty();

            RuleFor(x => x.Password)
                .NotEmpty()
                .MinimumLength(6);
        }
    }
    public class ResetPasswordRequestModelValidator : AbstractValidator<ResetPasswordRequestModel>
    {
        public ResetPasswordRequestModelValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(x => x.Code)
                .NotEmpty();

            RuleFor(x => x.Password)
                .NotEmpty()
                .MinimumLength(6);
        }
    }
}
