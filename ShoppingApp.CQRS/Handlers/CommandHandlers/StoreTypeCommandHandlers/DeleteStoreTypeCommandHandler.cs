using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ShoppingApp.CQRS.Models.CommandModels.StoreTypeCommands;
using ShoppingApp.CQRS.Models.ResponseModels.StoreTypeResponseModels.CommandResponseModels;
using ShoppingApp.Services.DBServices.DBServiceInterfaces;
using ShoppingApp.Utils.InternalModels;

namespace ShoppingApp.CQRS.Handlers.CommandHandlers.CategoryCommandHandlers
{
    public class DeleteStoreTypeCommandHandler : IRequestHandler<DeleteStoreTypeCommand, DeleteStoreTypeRangeResponseModel>
    {
        private readonly IStoreTypeService _storeTypeService;

        public DeleteStoreTypeCommandHandler(IStoreTypeService storeTypeService)
        {
            _storeTypeService = storeTypeService;
        }

        public async Task<DeleteStoreTypeRangeResponseModel> Handle(DeleteStoreTypeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var deletedCount = await _storeTypeService.DeleteAsync(globalId: request.GlobalId);
                if (deletedCount > 0)
                {
                    return new DeleteStoreTypeRangeResponseModel
                    {
                        DeletedCount = deletedCount
                    };
                }
                return new DeleteStoreTypeRangeResponseModel
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
                return new DeleteStoreTypeRangeResponseModel
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
