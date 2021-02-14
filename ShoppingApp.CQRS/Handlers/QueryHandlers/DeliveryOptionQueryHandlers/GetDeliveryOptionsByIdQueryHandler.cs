using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ShoppingApp.CQRS.Models.QueryModels.DeliveryOptionQueryModels;
using ShoppingApp.CQRS.Models.ResponseModels.DeliveryOptionResponseModels.QueryResponseModels;
using ShoppingApp.Services.DBServices.DBServiceInterfaces;
using ShoppingApp.Utils.InternalModels;

namespace ShoppingApp.CQRS.Handlers.QueryHandlers
{
    public class GetDeliveryOptionsByIdQueryHandler : IRequestHandler<GetDeliveryOptionByIdQuery, GetDeliveryOptionResponseModel>
    {
        private readonly IDeliveryOptionService _deliveryOptionService;

        public GetDeliveryOptionsByIdQueryHandler(IDeliveryOptionService deliveryOptionsService)
        {
            _deliveryOptionService = deliveryOptionsService;
        }

        public async Task<GetDeliveryOptionResponseModel> Handle(GetDeliveryOptionByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var deliveryOptions = await _deliveryOptionService.FindByGobalIdAsync(globalId: request.GlobalId);
                if (!(deliveryOptions is null))
                {
                    return new GetDeliveryOptionResponseModel
                    {
                        DeliveryOption = deliveryOptions,
                        HasError = false
                    };
                }
                return new GetDeliveryOptionResponseModel
                {
                    HasError = true,
                    ErrorType = Utils.Enums.ErrorType.Model,
                    Errors = new List<InternalErrorModel>
                        {
                            new InternalErrorModel
                            {
                                Message ="Not found"
                            }
                        }
                };
            }
            catch (Exception ex)
            {
                return new GetDeliveryOptionResponseModel
                {
                    HasError = true,
                    ErrorType = Utils.Enums.ErrorType.Exception,
                    Errors = new List<InternalErrorModel>
                        {
                            new InternalErrorModel
                            {
                                Message = ex.InnerException?.Message
                            }
                        }
                };
            }
        }
    }
}
