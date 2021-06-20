using System;
using System.Collections.Generic;
using MediatR;
using Microsoft.AspNetCore.Http;
using ShoppingApp.CQRS.Models.ResponseModels.ProductResponseModels;
using ShoppingApp.Domain.Models.Domain.StoreModels;

namespace ShoppingApp.CQRS.Models.CommandModels.ProductCommands
{
    public class CreateProductCommand : IRequest<CreateProductResponseModel>
    {
        public CreateProductCommand(
            string productTitle, string productSlug,
            string productDescription, string categoryId,
            List<ProductSize> sizes, List<IFormFile> images,
            List<string> tags, Store store, string shortDescription)
        {
            ProductTitle = productTitle;
            ProductSlug = productSlug;
            ProductDescription = productDescription;
            CategoryId = categoryId;
            Sizes = sizes;
            Images = images;
            Tags = tags;
            Store = store;
            ShortDescription = shortDescription;
        }
        public Store Store { get; set; }
        public string ProductTitle { get; set; }
        public string ProductSlug { get; set; }
        public string ProductDescription { get; set; }
        public string ShortDescription { get; set; }
        public string CategoryId { get; set; }

        public List<ProductSize> Sizes { get; set; }

        public List<IFormFile> Images { get; set; }

        public List<string> Tags { get; set; }
    }

    public class ProductSize
    {
        public ProductSize(
            string size, double price,
            double discountedPrice, int stockQuantity,
            string[] colorIds)
        {
            Size = size;
            Price = price;
            DiscountedPrice = discountedPrice;
            StockQuantity = stockQuantity;
            ColorIds = colorIds;
        }

        public string Size { get; set; }
        public double Price { get; set; }
        public double DiscountedPrice { get; set; }
        public int StockQuantity { get; set; }
        public string[] ColorIds { get; set; }
    }
}
