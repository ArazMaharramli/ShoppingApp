using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ShoppingApp.CQRS.Models.QueryModels.CountryQueryModels;
using ShoppingApp.CQRS.Models.ResponseModels.CountryResponseModels.QueryResponseModels;
using ShoppingApp.Services.DBServices.DBServiceInterfaces;
using ShoppingApp.Utils.InternalModels;

namespace ShoppingApp.CQRS.Handlers.QueryHandlers
{
    public class GetCountryByIdQueryHandler : IRequestHandler<GetCountryByIdQuery, GetCountryResponseModel>
    {
        private readonly ICountryService _countryService;

        public GetCountryByIdQueryHandler(ICountryService countryService)
        {
            _countryService = countryService;
        }

        public async Task<GetCountryResponseModel> Handle(GetCountryByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var country = await _countryService.GetWithCitiesAsync(countryId: request.GlobalId);
                if (!(country is null))
                {
                    if (!(country is null))
                    {
                        return new GetCountryResponseModel
                        {
                            Country = country,
                            HasError = false
                        };
                    }
                }
                return new GetCountryResponseModel
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
                return new GetCountryResponseModel
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
