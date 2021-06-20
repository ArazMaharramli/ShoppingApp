using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ShoppingApp.CQRS.Models.QueryModels.ColorQueryModels;
using ShoppingApp.CQRS.Models.ResponseModels.ColorResponseModels.QueryResponseModels;
using ShoppingApp.Services.DBServices.DBServiceInterfaces;
using ShoppingApp.Utils.InternalModels;

namespace ShoppingApp.CQRS.Handlers.QueryHandlers
{
    public class GetColorByIdQueryHandler : IRequestHandler<GetColorByIdQuery, GetColorResponseModel>
    {
        private readonly IColorService _colorService;

        public GetColorByIdQueryHandler(IColorService colorService)
        {
            _colorService = colorService;
        }

        public async Task<GetColorResponseModel> Handle(GetColorByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var storeType = await _colorService.FindByGobalIdAsync(globalId: request.GlobalId);
                if (!(storeType is null))
                {
                    if (!(storeType is null))
                    {
                        return new GetColorResponseModel
                        {
                            Color = storeType,
                            HasError = false
                        };
                    }
                }
                return new GetColorResponseModel
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
                return new GetColorResponseModel
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
