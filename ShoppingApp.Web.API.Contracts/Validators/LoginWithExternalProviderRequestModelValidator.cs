using FluentValidation;
using ShoppingApp.Web.API.Contracts.RequestModels.V1;

namespace ShoppingApp.Web.API.Contracts.Validators
{
    public class LoginWithExternalProviderRequestModelValidator : AbstractValidator<LoginWithExternalProviderRequestModel>
    {
        public LoginWithExternalProviderRequestModelValidator()
        {
            RuleFor(x => x.Token)
                .NotEmpty()
                .NotNull();
        }
    }
}
