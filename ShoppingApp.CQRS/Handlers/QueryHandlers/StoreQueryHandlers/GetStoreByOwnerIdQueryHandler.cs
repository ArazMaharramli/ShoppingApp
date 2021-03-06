﻿using System;
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
    public class GetStoreByOwnerIdQueryHandler : IRequestHandler<GetStoreByOwnerIdQuery, GetStoreResponseModel>
    {
        public IStoreService _storeService { get; set; }

        public GetStoreByOwnerIdQueryHandler(IStoreService storeService)
        {
            _storeService = storeService;
        }

        public async Task<GetStoreResponseModel> Handle(GetStoreByOwnerIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var store = await _storeService.FindByOwnerIdAsync(request.OwnerId);
                if (!(store is null))
                {
                    return new GetStoreResponseModel { Store = store };
                }
                return new GetStoreResponseModel
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
                return new GetStoreResponseModel
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
