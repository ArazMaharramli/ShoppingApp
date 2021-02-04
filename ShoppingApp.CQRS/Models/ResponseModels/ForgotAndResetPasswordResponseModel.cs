using System;
namespace ShoppingApp.CQRS.Models.ResponseModels
{
    public class ForgotAndResetPasswordResponseModel : BaseResponseModel
    {
        public string Code { get; set; }
    }
}
