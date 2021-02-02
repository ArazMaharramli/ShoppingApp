using System;
using System.Collections.Generic;
using ShoppingApp.Utils.InternalModels;

namespace ShoppingApp.CQRS.Models.ResponseModels
{
    public class BaseResponseModel
    {
        public bool HasError { get; set; }
        public IEnumerable<InternalErrorModel> Errors { get; set; }
    }
}
