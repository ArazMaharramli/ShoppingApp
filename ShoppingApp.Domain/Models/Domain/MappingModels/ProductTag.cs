using System;
using ShoppingApp.Domain.Models.Base;
using ShoppingApp.Domain.Models.Domain.ProductModels;
using ShoppingApp.Utils.Enums;

namespace ShoppingApp.Domain.Models.Domain.MappingModels
{
    public class ProductTag : BaseEntity<Status>
    {
        public string ProductId { get; set; }
        public Product Product { get; set; }
        public string TagId { get; set; }
        public Tag Tag { get; set; }
    }
}
