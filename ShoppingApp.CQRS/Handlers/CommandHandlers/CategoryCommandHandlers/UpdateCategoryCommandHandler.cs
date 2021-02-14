using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ShoppingApp.CQRS.Models.CommandModels.CategoryCommands;
using ShoppingApp.CQRS.Models.ResponseModels.CategoryResponseModels.CommandResponseModels;
using ShoppingApp.Domain.Models.Domain.ProductModels;
using ShoppingApp.Services.DBServices.DBServiceInterfaces;
using ShoppingApp.Utils.InternalModels;

namespace ShoppingApp.CQRS.Handlers.CommandHandlers.CategoryCommandHandlers
{
    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, UpdateCategoryResponseModel>
    {
        private readonly ICategoryService _categoryService;

        public UpdateCategoryCommandHandler(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<UpdateCategoryResponseModel> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var categoryInDb = await _categoryService.FindByGobalIdAsync(globalId: request.GlobalId);
                if (!(categoryInDb is null))
                {
                    IEnumerable<Category> children = new List<Category>();

                    categoryInDb.UniqueName = request.Name.Trim();
                    categoryInDb.UniqueSlug = request.Slug.Trim().ToLower();

                    if (!(request.ParentId is null))
                    {
                        categoryInDb.Parent = await _categoryService.FindByGobalIdAsync(globalId: request.ParentId); ;
                    }
                    if (!(request.ChildrenIds is null))
                    {
                        children = await _categoryService.FindRangeAsync(globalIds: request.ChildrenIds);

                    }
                    categoryInDb.Children = children?.ToList();
                    categoryInDb = await _categoryService.UpdateAsync(categoryInDb);
                    return new UpdateCategoryResponseModel
                    {
                        Category = categoryInDb
                    };
                }
                return new UpdateCategoryResponseModel
                {
                    HasError = true,
                    ErrorType = Utils.Enums.ErrorType.Model,
                    Errors = new List<InternalErrorModel>
                    {
                        new InternalErrorModel
                        {
                            Message = "Not Found"
                        }
                    }
                };
            }
            catch (System.Exception ex)
            {
                return new UpdateCategoryResponseModel
                {
                    HasError = true,
                    ErrorType = Utils.Enums.ErrorType.Exception,
                    Errors = new List<InternalErrorModel>
                    {
                        new InternalErrorModel
                        {
                            Message = ex.Message
                        }
                    }
                };
            }
        }
    }

}
