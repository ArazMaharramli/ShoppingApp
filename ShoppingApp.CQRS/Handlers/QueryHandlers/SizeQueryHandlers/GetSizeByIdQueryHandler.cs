using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ShoppingApp.CQRS.Models.QueryModels.SizeQueryModels;
using ShoppingApp.CQRS.Models.ResponseModels.SizeResponseModels.QueryResponseModels;
using ShoppingApp.Services.DBServices.DBServiceInterfaces;
using ShoppingApp.Utils.InternalModels;

namespace ShoppingApp.CQRS.Handlers.QueryHandlers
{
    public class GetSizeByIdQueryHandler : IRequestHandler<GetSizeByIdQuery, GetSizeResponseModel>
    {
        private readonly ISizeService _sizeService;

        public GetSizeByIdQueryHandler(ISizeService sizeService)
        {
            _sizeService = sizeService;
        }

        public async Task<GetSizeResponseModel> Handle(GetSizeByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var storeType = await _sizeService.FindByGobalIdAsync(globalId: request.GlobalId);
                if (!(storeType is null))
                {
                    if (!(storeType is null))
                    {
                        return new GetSizeResponseModel
                        {
                            Size = storeType,
                            HasError = false
                        };
                    }
                }
                return new GetSizeResponseModel
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
                return new GetSizeResponseModel
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
