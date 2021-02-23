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
    public class GetStoreByIdQueryHandler : IRequestHandler<GetStoreByIdQuery, GetStoreByIdResponseModel>
    {
        public IStoreService _storeService { get; set; }

        public GetStoreByIdQueryHandler(IStoreService storeService)
        {
            _storeService = storeService;
        }

        public async Task<GetStoreByIdResponseModel> Handle(GetStoreByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var store = await _storeService.FindByIdAsync(request.StoreId);
                if (!(store is null))
                {
                    return new GetStoreByIdResponseModel { Store = store };
                }
                return new GetStoreByIdResponseModel
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
                return new GetStoreByIdResponseModel
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
