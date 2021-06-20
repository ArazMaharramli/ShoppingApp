using System.Collections.Generic;
using ShoppingApp.Domain.Models.Domain.ProductModels;
using ShoppingApp.Utils.InternalModels;

namespace ShoppingApp.CQRS.Models.ResponseModels.ColorResponseModels.CommandResponseModels
{
    public class UpdateColorStatusRangeResponseModel : BaseResponseModel
    {
        public List<Color> Colors { get; set; }
    }
}
