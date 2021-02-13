using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ShoppingApp.CQRS.Models.QueryModels.StoreTypeQueryModels;
using ShoppingApp.CQRS.Models.ResponseModels.StoreTypeResponseModels.QueryResponseModels;
using ShoppingApp.Services.DBServices.DBServiceInterfaces;
using ShoppingApp.Utils.InternalModels;

namespace ShoppingApp.CQRS.Handlers.QueryHandlers.StoreTypeQueryHandlers
{
    public class GetPagedStoreTypesQueryHandler : IRequestHandler<GetPagedStoreTypesQuery, GetPagedStoreTypesResponseModel>
    {
        private readonly IStoreTypeService _StoreTypeService;
        public GetPagedStoreTypesQueryHandler(IStoreTypeService StoreTypeService)
        {
            _StoreTypeService = StoreTypeService;
        }

        public async Task<GetPagedStoreTypesResponseModel> Handle(GetPagedStoreTypesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var storeTypes = await _StoreTypeService.GetPagedAsync(
                    searchString: request.SearchString,
                    pageSize: request.PageSize,
                    pageNumber: request.PageNumber,
                    sortColumn: request.SortColumn,
                    sortDirection: request.SortDirection,
                    status: request.Status);

                if (storeTypes.Data != null)
                {
                    return new GetPagedStoreTypesResponseModel
                    {
                        StoreTypes = storeTypes
                    };
                }
                return new GetPagedStoreTypesResponseModel
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
                return new GetPagedStoreTypesResponseModel
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
