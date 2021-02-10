using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ShoppingApp.CQRS.Models.CommandModels.CategoryCommands;
using ShoppingApp.CQRS.Models.ResponseModels;
using ShoppingApp.Services.DBServices.DBServiceInterfaces;
using ShoppingApp.Utils.InternalModels;

namespace ShoppingApp.CQRS.Handlers.CommandHandlers.CategoryCommandHandlers
{
    public class UpdateCategoryRangeStatusCommandHandler : IRequestHandler<UpdateCategoryStatusRangeCommand, UpdateCategoryStatusRangeResponseModel>
    {
        private readonly ICategoryService _categoryService;

        public UpdateCategoryRangeStatusCommandHandler(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<UpdateCategoryStatusRangeResponseModel> Handle(UpdateCategoryStatusRangeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var updatedCategories = await _categoryService.UpdateStatusRangeAsync(globalIds: request.GlobalIds, status: request.Status);
                if (!(updatedCategories is null))
                {
                    if (updatedCategories.Count > 0)
                    {
                        return new UpdateCategoryStatusRangeResponseModel
                        {
                            Categories = updatedCategories
                        };
                    }

                }
                return new UpdateCategoryStatusRangeResponseModel
                {
                    HasError = true,
                    ErrorType = Utils.Enums.ErrorType.Model,
                    Errors = new List<InternalErrorModel>
                    {
                        new InternalErrorModel
                        {
                            Message = "Update failed"
                        }
                    }
                };
            }
            catch (System.Exception ex)
            {
                return new UpdateCategoryStatusRangeResponseModel
                {
                    HasError = true,
                    ErrorType = Utils.Enums.ErrorType.Model,
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
