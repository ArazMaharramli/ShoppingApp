using FluentValidation;
using ShoppingApp.Web.API.Contracts.RequestModels.V1;

namespace ShoppingApp.Web.API.Contracts.Validators
{
    public class RefreshTokenRequestModelValidator : AbstractValidator<RefreshTokenRequestModel>
    {
        public RefreshTokenRequestModelValidator()
        {
            RuleFor(x => x.JwtToken)
                .NotEmpty();

            RuleFor(x => x.RefreshToken)
                .NotEmpty();
        }
    }
}
