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
    public class CreateDeliveryOptionCommandHandler : IRequestHandler<CreateDeliveryOptionCommand, CreateDeliveryOptionResponseModel>
    {
        private readonly IDeliveryOptionService _DeliveryOptionService;

        public CreateDeliveryOptionCommandHandler(IDeliveryOptionService DeliveryOptionService)
        {
            _DeliveryOptionService = DeliveryOptionService;
        }

        public async Task<CreateDeliveryOptionResponseModel> Handle(CreateDeliveryOptionCommand request, CancellationToken cancellationToken)
        {
            var deliveryOptionName = request.UniqueName.Trim();
            var deliveryOptionInDb = await _DeliveryOptionService.FindByNameAndStatusAsync(deliveryOptionName);
            if (deliveryOptionInDb is null)
            {

                var deliveryOption = await _DeliveryOptionService.CreateAsync(name: deliveryOptionName, description: request.Description);

                if (!(deliveryOption is null))
                {
                    return new CreateDeliveryOptionResponseModel
                    {
                        DeliveryOption = deliveryOption
                    };
                }
                return new CreateDeliveryOptionResponseModel
                {
                    HasError = true,
                    ErrorType = Utils.Enums.ErrorType.Exception,
                };
            }
            return new CreateDeliveryOptionResponseModel
            {
                HasError = true,
                ErrorType = Utils.Enums.ErrorType.Model,
                Errors = new List<InternalErrorModel>
                {
                    new InternalErrorModel
                    {
                        Message = "Already exists"
                    }
                }
            };
        }
    }
}
