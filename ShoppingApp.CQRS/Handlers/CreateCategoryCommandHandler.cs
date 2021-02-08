using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ShoppingApp.CQRS.Models.CommandModels;
using ShoppingApp.CQRS.Models.ResponseModels;
using ShoppingApp.Domain.Models.Domain.ProductModels;
using ShoppingApp.Services.DBServices.DBServiceInterfaces;
using ShoppingApp.Utils.InternalModels;

namespace ShoppingApp.CQRS.Handlers
{
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, CreateCategoryResponseModel>
    {
        private readonly ICategoryService _categoryService;

        public CreateCategoryCommandHandler(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<CreateCategoryResponseModel> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var categoryName = request.CategoryName.Trim();
            var categoryInDb = await _categoryService.FindByNameAndStatusAsync(categoryName);
            if (categoryInDb is null)
            {
                Category parentCategory = null;
                if (!string.IsNullOrEmpty(request.ParentId))
                {
                    parentCategory = await _categoryService.FindByGobalIdAsync(request.ParentId);
                }

                var category = await _categoryService.CreateAsync(categoryName, request.Slug, parentCategory);

                if (category != null)
                {
                    return new CreateCategoryResponseModel
                    {
                        Category = category
                    };
                }
                return new CreateCategoryResponseModel
                {
                    HasError = true,
                    ErrorType = Utils.Enums.ErrorType.Exception,
                };
            }
            return new CreateCategoryResponseModel
            {
                HasError = true,
                ErrorType = Utils.Enums.ErrorType.Model,
                Errors = new List<InternalErrorModel>
                {
                    new InternalErrorModel
                    {
                        Message = "Already exists"
                    }
                }
            };
        }
    }
}
