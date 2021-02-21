using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ShoppingApp.CQRS.Models.QueryModels.StoreQueryModels;
using ShoppingApp.CQRS.Models.ResponseModels.StoreResponseModels.QueryResponseModels;
using ShoppingApp.Services.DBServices.DBServiceInterfaces;
using ShoppingApp.Utils.Enums;
using ShoppingApp.Utils.InternalModels;

namespace ShoppingApp.CQRS.Handlers.QueryHandlers.StoreQueryHandlers
{
    public class GetPagedStoresQueryHandler : IRequestHandler<GetPagedStoresQuery, GetPagedStoresResponseModel>
    {
        private readonly IStoreService _storeService;
        public GetPagedStoresQueryHandler(IStoreService storeService)
        {
            _storeService = storeService;
        }

        public async Task<GetPagedStoresResponseModel> Handle(GetPagedStoresQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var stores = await _storeService.GetPagedAsync(
                    searchString: request.SearchString,
                    pageSize: request.PageSize,
                    pageNumber: request.PageNumber,
                    sortColumn: request.SortColumn,
                    sortDirection: request.SortDirection,
                    status: request.Status);

                if (stores.Data != null)
                {
                    return new GetPagedStoresResponseModel
                    {
                        Stores = stores
                    };
                }
                return new GetPagedStoresResponseModel
                {
                    HasError = true,
                    ErrorType = ErrorType.Model,
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
                return new GetPagedStoresResponseModel
                {
                    HasError = true,
                    ErrorType = ErrorType.Exception,
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
