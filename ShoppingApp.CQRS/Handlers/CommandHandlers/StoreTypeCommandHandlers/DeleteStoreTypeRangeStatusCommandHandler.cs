using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ShoppingApp.CQRS.Models.CommandModels.StoreTypeCommands;
using ShoppingApp.CQRS.Models.ResponseModels.StoreTypeResponseModels.CommandResponseModels;
using ShoppingApp.Services.DBServices.DBServiceInterfaces;
using ShoppingApp.Utils.InternalModels;

namespace ShoppingApp.CQRS.Handlers.CommandHandlers.StoreTypeCommandHandlers
{
    public class DeleteStoreTypeRangeStatusCommandHandler : IRequestHandler<DeleteStoreTypeRangeCommand, DeleteStoreTypeRangeResponseModel>
    {
        private readonly IStoreTypeService _storeTypeService;

        public DeleteStoreTypeRangeStatusCommandHandler(IStoreTypeService storeTypeService)
        {
            _storeTypeService = storeTypeService;
        }

        public async Task<DeleteStoreTypeRangeResponseModel> Handle(DeleteStoreTypeRangeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var deletedCount = await _storeTypeService.DeleteRangeAsync(globalIds: request.GlobalIds);
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
