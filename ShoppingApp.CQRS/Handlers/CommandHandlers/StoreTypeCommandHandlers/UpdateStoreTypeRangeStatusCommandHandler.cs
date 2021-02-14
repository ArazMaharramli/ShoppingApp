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
    public class UpdateStoreTypeRangeStatusCommandHandler : IRequestHandler<UpdateStoreTypeStatusRangeCommand, UpdateStoreTypeStatusRangeResponseModel>
    {
        private readonly IStoreTypeService _storeTypeService;

        public UpdateStoreTypeRangeStatusCommandHandler(IStoreTypeService storeTypeService)
        {
            _storeTypeService = storeTypeService;
        }

        public async Task<UpdateStoreTypeStatusRangeResponseModel> Handle(UpdateStoreTypeStatusRangeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var storeTypes = await _storeTypeService.UpdateStatusRangeAsync(globalIds: request.GlobalIds, status: request.Status);
                if (!(storeTypes is null))
                {
                    if (storeTypes.Count > 0)
                    {
                        return new UpdateStoreTypeStatusRangeResponseModel
                        {
                            StoreTypes = storeTypes
                        };
                    }

                }
                return new UpdateStoreTypeStatusRangeResponseModel
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
                return new UpdateStoreTypeStatusRangeResponseModel
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
