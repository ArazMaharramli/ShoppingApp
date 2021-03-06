using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ShoppingApp.CQRS.Models.QueryModels.SizeQueryModels;
using ShoppingApp.CQRS.Models.ResponseModels.SizeResponseModels.QueryResponseModels;
using ShoppingApp.Services.DBServices.DBServiceInterfaces;
using ShoppingApp.Utils.InternalModels;

namespace ShoppingApp.CQRS.Handlers.QueryHandlers.SizeQueryHandlers
{
    public class GetSizesQueryHandler : IRequestHandler<GetSizesQuery, GetSizesResponseModel>
    {
        private readonly ISizeService _sizeService;

        public GetSizesQueryHandler(ISizeService sizeService)
        {
            _sizeService = sizeService;
        }


        public async Task<GetSizesResponseModel> Handle(GetSizesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var sizes = await _sizeService.GetAllActivesAsync();
                if (!(sizes is null))
                {
                    if (sizes.ToList().Count > 0)
                    {
                        return new GetSizesResponseModel
                        {
                            Sizes = sizes,
                            HasError = false
                        };
                    }
                }
                return new GetSizesResponseModel
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
                return new GetSizesResponseModel
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
