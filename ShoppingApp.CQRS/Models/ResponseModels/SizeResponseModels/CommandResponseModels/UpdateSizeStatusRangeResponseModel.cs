using System.Collections.Generic;
using ShoppingApp.Domain.Models.Domain.ProductModels;
using ShoppingApp.Utils.InternalModels;

namespace ShoppingApp.CQRS.Models.ResponseModels.SizeResponseModels.CommandResponseModels
{
    public class UpdateSizeStatusRangeResponseModel : BaseResponseModel
    {
        public List<Size> Sizes { get; set; }
    }
}
