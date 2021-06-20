using System.Collections.Generic;
using ShoppingApp.Domain.Models.Domain.ProductModels;
using ShoppingApp.Utils.InternalModels;

namespace ShoppingApp.CQRS.Models.ResponseModels.ColorResponseModels.QueryResponseModels
{
    public class GetColorsResponseModel : BaseResponseModel
    {
        public IEnumerable<Color> Colors { get; set; }
    }
}
