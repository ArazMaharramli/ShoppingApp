using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ShoppingApp.CQRS.Models.QueryModels.DeliveryOptionQueryModels;
using ShoppingApp.CQRS.Models.ResponseModels.DeliveryOptionResponseModels.QueryResponseModels;
using ShoppingApp.Services.DBServices.DBServiceInterfaces;
using ShoppingApp.Utils.InternalModels;

namespace ShoppingApp.CQRS.Handlers.QueryHandlers.DeliveryOptionQueryHandlers
{
    public class GetPagedDeliveryOptionsQueryHandler : IRequestHandler<GetPagedDeliveryOptionsQuery, GetPagedDeliveryOptionsResponseModel>
    {
        private readonly IDeliveryOptionService _deliveryOptionservice;
        public GetPagedDeliveryOptionsQueryHandler(IDeliveryOptionService DeliveryOptionService)
        {
            _deliveryOptionservice = DeliveryOptionService;
        }

        public async Task<GetPagedDeliveryOptionsResponseModel> Handle(GetPagedDeliveryOptionsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var deliveryOptions = await _deliveryOptionservice.GetPagedAsync(
                    searchString: request.SearchString,
                    pageSize: request.PageSize,
                    pageNumber: request.PageNumber,
                    sortColumn: request.SortColumn,
                    sortDirection: request.SortDirection,
                    status: request.Status);

                if (deliveryOptions.Data != null)
                {
                    return new GetPagedDeliveryOptionsResponseModel
                    {
                        DeliveryOptions = deliveryOptions
                    };
                }
                return new GetPagedDeliveryOptionsResponseModel
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
            catch (Exception ex)
            {
                return new GetPagedDeliveryOptionsResponseModel
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
