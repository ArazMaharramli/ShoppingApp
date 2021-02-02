using System.Collections.Generic;

namespace ShoppingApp.Web.API.Contracts.ResponseModels.V1
{
    public class ErrorListResponseModel
    {
        public IEnumerable<ErrorResponseModel> Errors { get; set; } = new List<ErrorResponseModel>();
    }
}
