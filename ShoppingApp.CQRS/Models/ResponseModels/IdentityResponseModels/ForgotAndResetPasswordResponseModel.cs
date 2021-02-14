using ShoppingApp.Utils.InternalModels;

namespace ShoppingApp.CQRS.Models.ResponseModels.IdentityResponseModels
{
    public class ForgotAndResetPasswordResponseModel : BaseResponseModel
    {
        public string Message { get; set; }
    }
}
