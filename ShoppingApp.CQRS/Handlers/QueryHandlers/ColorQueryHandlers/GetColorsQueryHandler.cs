using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ShoppingApp.CQRS.Models.QueryModels.ColorQueryModels;
using ShoppingApp.CQRS.Models.ResponseModels.ColorResponseModels.QueryResponseModels;
using ShoppingApp.Services.DBServices.DBServiceInterfaces;
using ShoppingApp.Utils.InternalModels;

namespace ShoppingApp.CQRS.Handlers.QueryHandlers.ColorQueryHandlers
{
    public class GetColorsQueryHandler : IRequestHandler<GetColorsQuery, GetColorsResponseModel>
    {
        private readonly IColorService _colorService;

        public GetColorsQueryHandler(IColorService colorService)
        {
            _colorService = colorService;
        }


        public async Task<GetColorsResponseModel> Handle(GetColorsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var colors = await _colorService.GetAllActivesAsync();
                if (!(colors is null))
                {
                    if (colors.ToList().Count > 0)
                    {
                        return new GetColorsResponseModel
                        {
                            Colors = colors,
                            HasError = false
                        };
                    }
                }
                return new GetColorsResponseModel
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
                return new GetColorsResponseModel
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
