using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ShoppingApp.CQRS.Models.CommandModels.DeliveryOptionCommands;
using ShoppingApp.CQRS.Models.ResponseModels.DeliveryOptionResponseModels.CommandResponseModels;
using ShoppingApp.Domain.Models.Domain.ProductModels;
using ShoppingApp.Services.DBServices.DBServiceInterfaces;
using ShoppingApp.Utils.InternalModels;

namespace ShoppingApp.CQRS.Handlers.CommandHandlers.DeliveryOptionCommandHandlers
{
    public class UpdateDeliveryOptionCommandHandler : IRequestHandler<UpdateDeliveryOptionCommand, UpdateDeliveryOptionResponseModel>
    {
        private readonly IDeliveryOptionService _deliveryOptionService;

        public UpdateDeliveryOptionCommandHandler(IDeliveryOptionService deliveryOptionService)
        {
            _deliveryOptionService = deliveryOptionService;
        }

        public async Task<UpdateDeliveryOptionResponseModel> Handle(UpdateDeliveryOptionCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var deliveryOptionInDb = await _deliveryOptionService.FindByGobalIdAsync(globalId: request.GlobalId);
                if (!(deliveryOptionInDb is null))
                {
                    deliveryOptionInDb.UniqueName = request.UniqueName.Trim();
                    deliveryOptionInDb.Description = request.Description.Trim();
                    deliveryOptionInDb = await _deliveryOptionService.UpdateAsync(deliveryOptionInDb);
                    return new UpdateDeliveryOptionResponseModel
                    {
                        DeliveryOption = deliveryOptionInDb
                    };
                }
                return new UpdateDeliveryOptionResponseModel
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
                return new UpdateDeliveryOptionResponseModel
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
