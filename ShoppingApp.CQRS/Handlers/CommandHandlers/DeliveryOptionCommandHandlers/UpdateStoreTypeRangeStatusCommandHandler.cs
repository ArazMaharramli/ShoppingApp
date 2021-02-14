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
    public class UpdateDeliveryOptionRangeStatusCommandHandler : IRequestHandler<UpdateDeliveryOptionStatusRangeCommand, UpdateDeliveryOptionStatusRangeResponseModel>
    {
        private readonly IDeliveryOptionService _deliveryOptionService;

        public UpdateDeliveryOptionRangeStatusCommandHandler(IDeliveryOptionService deliveryOptionService)
        {
            _deliveryOptionService = deliveryOptionService;
        }

        public async Task<UpdateDeliveryOptionStatusRangeResponseModel> Handle(UpdateDeliveryOptionStatusRangeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var DeliveryOptions = await _deliveryOptionService.UpdateStatusRangeAsync(globalIds: request.GlobalIds, status: request.Status);
                if (!(DeliveryOptions is null))
                {
                    if (DeliveryOptions.Count > 0)
                    {
                        return new UpdateDeliveryOptionStatusRangeResponseModel
                        {
                            DeliveryOptions = DeliveryOptions
                        };
                    }

                }
                return new UpdateDeliveryOptionStatusRangeResponseModel
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
                return new UpdateDeliveryOptionStatusRangeResponseModel
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
