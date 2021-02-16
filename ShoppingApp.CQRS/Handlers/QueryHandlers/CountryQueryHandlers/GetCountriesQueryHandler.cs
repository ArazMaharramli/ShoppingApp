using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ShoppingApp.CQRS.Models.QueryModels.CountryQueryModels;
using ShoppingApp.CQRS.Models.ResponseModels.CountryResponseModels.QueryResponseModels;
using ShoppingApp.Services.DBServices.DBServiceInterfaces;
using ShoppingApp.Utils.InternalModels;

namespace ShoppingApp.CQRS.Handlers.QueryHandlers.CountryQueryHandlers
{
    public class GetCountriesQueryHandler : IRequestHandler<GetCountriesQuery, GetCountriesResponseModel>
    {
        private readonly ICountryService _countryService;

        public GetCountriesQueryHandler(ICountryService countryService)
        {
            _countryService = countryService;
        }


        public async Task<GetCountriesResponseModel> Handle(GetCountriesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var countries = await _countryService.GetAllAsync();
                if (!(countries is null))
                {
                    if (countries.ToList().Count > 0)
                    {
                        return new GetCountriesResponseModel
                        {
                            Countries = countries,
                            HasError = false
                        };
                    }
                }
                return new GetCountriesResponseModel
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
                return new GetCountriesResponseModel
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
