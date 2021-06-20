using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ShoppingApp.CQRS.Models.QueryModels.ColorQueryModels;
using ShoppingApp.CQRS.Models.ResponseModels.ColorResponseModels.QueryResponseModels;
using ShoppingApp.Services.DBServices.DBServiceInterfaces;
using ShoppingApp.Utils.InternalModels;

namespace ShoppingApp.CQRS.Handlers.QueryHandlers.ColorQueryHandlers
{
    public class GetPagedColorsQueryHandler : IRequestHandler<GetPagedColorsQuery, GetPagedColorsResponseModel>
    {
        private readonly IColorService _colorService;
        public GetPagedColorsQueryHandler(IColorService colorService)
        {
            _colorService = colorService;
        }

        public async Task<GetPagedColorsResponseModel> Handle(GetPagedColorsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var colors = await _colorService.GetPagedAsync(
                    searchString: request.SearchString,
                    pageColor: request.PageColor,
                    pageNumber: request.PageNumber,
                    sortColumn: request.SortColumn,
                    sortDirection: request.SortDirection,
                    status: request.Status);

                if (colors.Data != null)
                {
                    return new GetPagedColorsResponseModel
                    {
                        Colors = colors
                    };
                }
                return new GetPagedColorsResponseModel
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
                return new GetPagedColorsResponseModel
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
