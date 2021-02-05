using FluentValidation;
using ShoppingApp.Web.API.Contracts.RequestModels.V1;

namespace ShoppingApp.Web.API.Contracts.Validators
{
    public class ForgootPasswordRequestModelValidator : AbstractValidator<ForgotPasswordRequestModel>
    {
        public ForgootPasswordRequestModelValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();
        }
    }
}
