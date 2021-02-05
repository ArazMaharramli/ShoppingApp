using System.Threading;
using System.Threading.Tasks;
using System.Web;
using MediatR;
using ShoppingApp.CQRS.Models.CommandModels;
using ShoppingApp.CQRS.Models.ResponseModels;
using ShoppingApp.Services.DBServices.DBServiceInterfaces;
using ShoppingApp.Services.EmailServices;

namespace ShoppingApp.CQRS.Handlers
{
    public class ForgotPasswordCommandHandler : IRequestHandler<ForgotPasswordCommand, ForgotAndResetPasswordResponseModel>
    {
        private readonly IUserIdentityService _userIdentityService;
        private readonly IEmailSender _emailSender;

        public ForgotPasswordCommandHandler(IUserIdentityService userIdentityService, IEmailSender emailSender)
        {
            _userIdentityService = userIdentityService;
            _emailSender = emailSender;
        }
        public async Task<ForgotAndResetPasswordResponseModel> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _userIdentityService.FindByEmailAsync(request.Email);
            if (!(user == null))
            {
                var code = await _userIdentityService.GeneratePasswordResetTokenAsync(user);
                var encodedcode = HttpUtility.UrlEncode(code);
                var callBackUrl = $"https://localhost:5005/Account/ResetPassword?userId={user.Id}&code={encodedcode}";
                await _emailSender.SendResetPasswordEmailAsync(request.Email, user.FirstName, callBackUrl);


                return new ForgotAndResetPasswordResponseModel
                {
                    Message = "Go to your email and click the link to reset your password!"
                };
            }
            return new ForgotAndResetPasswordResponseModel { HasError = true };

        }
    }

}
