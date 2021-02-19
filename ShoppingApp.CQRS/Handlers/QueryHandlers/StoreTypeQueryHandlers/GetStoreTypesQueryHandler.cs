using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ShoppingApp.CQRS.Models.QueryModels.StoreTypeQueryModels;
using ShoppingApp.CQRS.Models.ResponseModels.StoreTypeResponseModels.QueryResponseModels;
using ShoppingApp.Services.DBServices.DBServiceInterfaces;
using ShoppingApp.Utils.InternalModels;

namespace ShoppingApp.CQRS.Handlers.QueryHandlers.StoreTypeQueryHandlers
{
    public class GetStoreTypesQueryHandler : IRequestHandler<GetStoreTypesQuery, GetStoreTypesResponseModel>
    {
        private readonly IStoreTypeService _storeTypeService;

        public GetStoreTypesQueryHandler(IStoreTypeService storeTypeService)
        {
            _storeTypeService = storeTypeService;
        }


        public async Task<GetStoreTypesResponseModel> Handle(GetStoreTypesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var storeTypes = await _storeTypeService.GetAllActivesAsync();
                if (!(storeTypes is null))
                {
                    if (storeTypes.ToList().Count > 0)
                    {
                        return new GetStoreTypesResponseModel
                        {
                            StoreTypes = storeTypes,
                            HasError = false
                        };
                    }
                }
                return new GetStoreTypesResponseModel
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
                return new GetStoreTypesResponseModel
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
