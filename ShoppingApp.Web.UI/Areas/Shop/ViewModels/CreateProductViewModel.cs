using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ShoppingApp.Web.UI.Areas.Shop.ViewModels
{
    public class CreateProductViewModel
    {
        public string ProductTitle { get; set; }
        public string ProductSlug { get; set; }
        public string ProductDescription { get; set; }
        public string SelectedCategoryId { get; set; }

        public List<SizeModel> Sizes { get; set; }

        public List<IFormFile> Images { get; set; }

        public List<string> Tags { get; set; }

        public SelectList Categories { get; set; }
        public SelectList Colors { get; set; }
    }


    public class SizeModel
    {
        public string Size { get; set; }
        public double Price { get; set; }
        public double DiscountedPrice { get; set; }
        public int StockQuantity { get; set; }
        public string[] SelectedColorIds { get; set; }
    }
}
