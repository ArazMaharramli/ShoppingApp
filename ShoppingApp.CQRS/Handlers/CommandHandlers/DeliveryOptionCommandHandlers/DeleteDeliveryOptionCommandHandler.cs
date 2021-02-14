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
    public class DeleteDeliveryOptionCommandHandler : IRequestHandler<DeleteDeliveryOptionCommand, DeleteDeliveryOptionRangeResponseModel>
    {
        private readonly IDeliveryOptionService _deliveryOptionService;

        public DeleteDeliveryOptionCommandHandler(IDeliveryOptionService deliveryOptionService)
        {
            _deliveryOptionService = deliveryOptionService;
        }

        public async Task<DeleteDeliveryOptionRangeResponseModel> Handle(DeleteDeliveryOptionCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var deletedCount = await _deliveryOptionService.DeleteAsync(globalId: request.GlobalId);
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
