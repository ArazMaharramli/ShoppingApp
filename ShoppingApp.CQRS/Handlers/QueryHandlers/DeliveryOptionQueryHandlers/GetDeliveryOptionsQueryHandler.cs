using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ShoppingApp.CQRS.Models.QueryModels.DeliveryOptionQueryModels;
using ShoppingApp.CQRS.Models.ResponseModels.DeliveryOptionResponseModels.QueryResponseModels;
using ShoppingApp.Services.DBServices.DBServiceInterfaces;
using ShoppingApp.Utils.InternalModels;

namespace ShoppingApp.CQRS.Handlers.QueryHandlers.DeliveryOptionQueryHandlers
{
    public class GetDeliveryOptionsQueryHandler : IRequestHandler<GetDeliveryOptionsQuery, GetDeliveryOptionsResponseModel>
    {
        private readonly IDeliveryOptionService _deliveryOptionService;

        public GetDeliveryOptionsQueryHandler(IDeliveryOptionService deliveryOptionService)
        {
            _deliveryOptionService = deliveryOptionService;
        }


        public async Task<GetDeliveryOptionsResponseModel> Handle(GetDeliveryOptionsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var deliveryOptions = await _deliveryOptionService.GetAllAsync();
                if (!(deliveryOptions is null))
                {
                    if (deliveryOptions.ToList().Count > 0)
                    {
                        return new GetDeliveryOptionsResponseModel
                        {
                            DeliveryOptions = deliveryOptions,
                            HasError = false
                        };
                    }
                }
                return new GetDeliveryOptionsResponseModel
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
                return new GetDeliveryOptionsResponseModel
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
