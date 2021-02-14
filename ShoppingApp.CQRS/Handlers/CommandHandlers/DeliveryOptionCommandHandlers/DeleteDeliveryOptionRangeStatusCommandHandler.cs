using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ShoppingApp.CQRS.Models.CommandModels.DeliveryOptionCommands;
using ShoppingApp.CQRS.Models.ResponseModels.DeliveryOptionResponseModels.CommandResponseModels;
using ShoppingApp.Services.DBServices.DBServiceInterfaces;
using ShoppingApp.Utils.InternalModels;

namespace ShoppingApp.CQRS.Handlers.CommandHandlers.DeliveryOptionCommandHandlers
{
    public class DeleteDeliveryOptionRangeStatusCommandHandler : IRequestHandler<DeleteDeliveryOptionRangeCommand, DeleteDeliveryOptionRangeResponseModel>
    {
        private readonly IDeliveryOptionService _deliveryOptionService;

        public DeleteDeliveryOptionRangeStatusCommandHandler(IDeliveryOptionService deliveryOptionService)
        {
            _deliveryOptionService = deliveryOptionService;
        }

        public async Task<DeleteDeliveryOptionRangeResponseModel> Handle(DeleteDeliveryOptionRangeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var deletedCount = await _deliveryOptionService.DeleteRangeAsync(globalIds: request.GlobalIds);
                if (deletedCount > 0)
                {
                    return new DeleteDeliveryOptionRangeResponseModel
                    {
                        DeletedCount = deletedCount
                    };
                }
                return new DeleteDeliveryOptionRangeResponseModel
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
                return new DeleteDeliveryOptionRangeResponseModel
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
