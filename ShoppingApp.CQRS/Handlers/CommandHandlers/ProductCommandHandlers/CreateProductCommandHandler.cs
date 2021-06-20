using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using ShoppingApp.CQRS.Models.CommandModels.ProductCommands;
using ShoppingApp.CQRS.Models.ResponseModels.ProductResponseModels;
using ShoppingApp.Domain.Models.Domain.MappingModels;
using ShoppingApp.Domain.Models.Domain.MediaModels;
using ShoppingApp.Domain.Models.Domain.ProductModels;
using ShoppingApp.Services.DBServices.DBServiceInterfaces;
using ShoppingApp.Services.FileServices;
using ShoppingApp.Utils.Enums;
using ShoppingApp.Utils.InternalModels;

namespace ShoppingApp.CQRS.Handlers.CommandHandlers.ProductCommandHandlers
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, CreateProductResponseModel>
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IFileService _fileService;
        private readonly IColorService _colorService;
        private readonly ISizeService _sizeService;
        private readonly ITagService _tagService;

        public CreateProductCommandHandler(
            IProductService productService,
            ICategoryService categoryService,
            IFileService fileService, IWebHostEnvironment webHostEnvironment,
            IColorService colorService,
            ISizeService sizeService,
            ITagService tagService)
        {
            _productService = productService;
            _categoryService = categoryService;
            _fileService = fileService;
            _webHostEnvironment = webHostEnvironment;
            _colorService = colorService;
            _sizeService = sizeService;
            _tagService = tagService;
        }

        public async Task<CreateProductResponseModel> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (!(await _productService.IsSlugAvailableAsync(slug: request.ProductSlug)))
                {
                    return ReturnError("slug is not available", ErrorType.Model);
                }

                var category = await _categoryService.FindByGobalIdAsync(request.CategoryId);
                if (category is null)
                {
                    return ReturnError("category not exists", ErrorType.Model);
                }
                List<string> productImageUrls = new List<string>();
                foreach (var image in request.Images)
                {
                    var url = await _fileService.UploadFileAsync(image, _webHostEnvironment.WebRootPath, $"ProductImages/{request.Store.UniqueSlug}/{request.ProductSlug}", $"{request.ProductSlug}");
                    productImageUrls.Add(url);
                }


                var productDetails = new List<ProductDetail>();
                foreach (var item in request.Sizes)
                {
                    var size = await _sizeService.FindByTitleAndStatusAsync(item.Size.ToLower());
                    if (size is null)
                    {
                        size = await _sizeService.CreateAsync(item.Size);
                    }
                    foreach (var colorId in item.ColorIds)
                    {
                        var color = await _colorService.FindByGobalIdAsync(colorId);
                        if (color is null)
                        {
                            return ReturnError("color not exists", ErrorType.Model);
                        }
                        var detail = new ProductDetail
                        {
                            Price = item.Price,
                            DiscountedPrice = item.DiscountedPrice,
                            StockQuantity = item.StockQuantity,
                            Color = color,
                            Size = size,
                        };
                        productDetails.Add(detail);
                    }
                }


                var product = new Product
                {
                    Title = request.ProductTitle.Trim(),
                    UniqueSlug = request.ProductSlug.Trim(),
                    Description = request.ProductDescription.Trim(),
                    ShortDescription = request.ShortDescription,
                    ProductCategories = new List<ProductCategory> { new ProductCategory { Category = category } },
                    ProductMedias = productImageUrls.Select(x => new ProductMedia
                    {
                        Url = x,
                        MediaType = MediaType.Image,
                    }).ToList(),
                    ProductDetails = productDetails,

                    Store = request.Store,
                    Status = ProductStatus.Active,
                };
                foreach (var item in request.Tags)
                {
                    var tag = await _tagService.GetByName(item);
                    if (tag is null)
                    {
                        tag = await _tagService.CreateAsync(item);
                    }
                    product.ProductTags.Add(new ProductTag { Tag = tag, Product = product });
                }

                return new CreateProductResponseModel
                {
                    Product = await _productService.CreateProductAsync(product)
                };

            }
            catch (Exception ex)
            {
                return ReturnError(ex.Message, ErrorType.Exception);
            }
        }

        private static CreateProductResponseModel ReturnError(string message, ErrorType type)
        {
            return new CreateProductResponseModel
            {
                HasError = true,
                ErrorType = type,
                Errors = new List<InternalErrorModel>
                {
                    new InternalErrorModel
                    {
                        Message = message
                    }
                }
            };
        }
    }
}
