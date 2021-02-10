﻿using System.Collections.Generic;
using ShoppingApp.Domain.Models.Domain.ProductModels;
using ShoppingApp.Utils.InternalModels;

namespace ShoppingApp.CQRS.Models.ResponseModels
{
    public class UpdateCategoryStatusRangeResponseModel : BaseResponseModel
    {
        public List<Category> Categories { get; set; }
    }
}
