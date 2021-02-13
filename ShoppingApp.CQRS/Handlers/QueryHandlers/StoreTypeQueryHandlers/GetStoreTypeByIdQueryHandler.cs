using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ShoppingApp.CQRS.Models.QueryModels.StoreTypeQueryModels;
using ShoppingApp.CQRS.Models.ResponseModels.StoreTypeResponseModels.QueryResponseModels;
using ShoppingApp.Services.DBServices.DBServiceInterfaces;
using ShoppingApp.Utils.InternalModels;

namespace ShoppingApp.CQRS.Handlers.QueryHandlers
{
    public class GetStoreTypeByIdQueryHandler : IRequestHandler<GetStoreTypeByIdQuery, GetStoreTypeResponseModel>
    {
        private readonly IStoreTypeService _StoreTypeService;

        public GetStoreTypeByIdQueryHandler(IStoreTypeService StoreTypeService)
        {
            _StoreTypeService = StoreTypeService;
        }

        public async Task<GetStoreTypeResponseModel> Handle(GetStoreTypeByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var storeType = await _StoreTypeService.FindByGobalIdAsync(globalId: request.GlobalId);
                if (!(storeType is null))
                {
                    if (!(storeType is null))
                    {
                        return new GetStoreTypeResponseModel
                        {
                            StoreType = storeType,
                            HasError = false
                        };
                    }
                }
                return new GetStoreTypeResponseModel
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
                return new GetStoreTypeResponseModel
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
