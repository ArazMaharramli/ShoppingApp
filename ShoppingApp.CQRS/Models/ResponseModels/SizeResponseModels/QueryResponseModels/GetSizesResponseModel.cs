using System.Collections.Generic;
using ShoppingApp.Domain.Models.Domain.ProductModels;
using ShoppingApp.Utils.InternalModels;

namespace ShoppingApp.CQRS.Models.ResponseModels.SizeResponseModels.QueryResponseModels
{
    public class GetSizesResponseModel : BaseResponseModel
    {
        public IEnumerable<Size> Sizes { get; set; }
    }
}
