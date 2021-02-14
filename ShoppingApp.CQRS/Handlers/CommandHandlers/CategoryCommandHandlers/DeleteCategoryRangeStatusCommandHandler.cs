using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ShoppingApp.CQRS.Models.CommandModels.CategoryCommands;
using ShoppingApp.CQRS.Models.ResponseModels.CategoryResponseModels.CommandResponseModels;
using ShoppingApp.Services.DBServices.DBServiceInterfaces;
using ShoppingApp.Utils.InternalModels;

namespace ShoppingApp.CQRS.Handlers.CommandHandlers.CategoryCommandHandlers
{
    public class DeleteCategoryRangeStatusCommandHandler : IRequestHandler<DeleteCategoryRangeCommand, DeleteCategoryRangeResponseModel>
    {
        private readonly ICategoryService _categoryService;

        public DeleteCategoryRangeStatusCommandHandler(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<DeleteCategoryRangeResponseModel> Handle(DeleteCategoryRangeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var deletedCount = await _categoryService.DeleteRangeAsync(globalIds: request.GlobalIds);
                if (deletedCount > 0)
                {
                    return new DeleteCategoryRangeResponseModel
                    {
                        DeletedCount = deletedCount
                    };
                }
                return new DeleteCategoryRangeResponseModel
                {
                    HasError = true,
                    ErrorType = Utils.Enums.ErrorType.Model,
                    Errors = new List<InternalErrorModel>
                    {
                        new InternalErrorModel
                        {
                            Message = "Delete failed"
                        }
                    }
                };
            }
            catch (System.Exception ex)
            {
                return new DeleteCategoryRangeResponseModel
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
