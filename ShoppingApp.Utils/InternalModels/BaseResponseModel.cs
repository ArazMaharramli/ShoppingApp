using System.Collections.Generic;
using ShoppingApp.Utils.Enums;

namespace ShoppingApp.Utils.InternalModels
{
    public class BaseResponseModel
    {
        public bool HasError { get; set; }
        public ErrorType ErrorType { get; set; }
        public IEnumerable<InternalErrorModel> Errors { get; set; }
    }
}
