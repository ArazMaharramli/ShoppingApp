using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ShoppingApp.CQRS.Models.QueryModels.SizeQueryModels;
using ShoppingApp.CQRS.Models.ResponseModels.SizeResponseModels.QueryResponseModels;
using ShoppingApp.Services.DBServices.DBServiceInterfaces;
using ShoppingApp.Utils.InternalModels;

namespace ShoppingApp.CQRS.Handlers.QueryHandlers.SizeQueryHandlers
{
    public class GetPagedSizesQueryHandler : IRequestHandler<GetPagedSizesQuery, GetPagedSizesResponseModel>
    {
        private readonly ISizeService _sizeService;
        public GetPagedSizesQueryHandler(ISizeService sizeService)
        {
            _sizeService = sizeService;
        }

        public async Task<GetPagedSizesResponseModel> Handle(GetPagedSizesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var sizes = await _sizeService.GetPagedAsync(
                    searchString: request.SearchString,
                    pageSize: request.PageSize,
                    pageNumber: request.PageNumber,
                    sortColumn: request.SortColumn,
                    sortDirection: request.SortDirection,
                    status: request.Status);

                if (sizes.Data != null)
                {
                    return new GetPagedSizesResponseModel
                    {
                        Sizes = sizes
                    };
                }
                return new GetPagedSizesResponseModel
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
                return new GetPagedSizesResponseModel
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
