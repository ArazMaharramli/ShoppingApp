using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ShoppingApp.CQRS.Models.QueryModels.CountryQueryModels;
using ShoppingApp.CQRS.Models.ResponseModels.CountryResponseModels.QueryResponseModels;
using ShoppingApp.Services.DBServices.DBServiceInterfaces;
using ShoppingApp.Utils.InternalModels;

namespace ShoppingApp.CQRS.Handlers.QueryHandlers.CountryQueryHandlers
{
    public class GetPagedCountriesQueryHandler : IRequestHandler<GetPagedCountriesQuery, GetPagedCountriesResponseModel>
    {
        private readonly ICountryService _countryService;
        public GetPagedCountriesQueryHandler(ICountryService countryService)
        {
            _countryService = countryService;
        }

        public async Task<GetPagedCountriesResponseModel> Handle(GetPagedCountriesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var Countries = await _countryService.GetPagedAsync(
                    searchString: request.SearchString,
                    pageSize: request.PageSize,
                    pageNumber: request.PageNumber,
                    sortColumn: request.SortColumn,
                    sortDirection: request.SortDirection,
                    status: request.Status);

                if (Countries.Data != null)
                {
                    return new GetPagedCountriesResponseModel
                    {
                        Countries = Countries
                    };
                }
                return new GetPagedCountriesResponseModel
                {
                    HasError = true,
                    ErrorType = Utils.Enums.ErrorType.Model,
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
                return new GetPagedCountriesResponseModel
                {
                    HasError = true,
                    ErrorType = Utils.Enums.ErrorType.Exception,
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
