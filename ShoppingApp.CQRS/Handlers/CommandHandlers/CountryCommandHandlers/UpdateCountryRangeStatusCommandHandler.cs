using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ShoppingApp.CQRS.Models.CommandModels.CountryCommands;
using ShoppingApp.CQRS.Models.ResponseModels.CountryResponseModels.CommandResponseModels;
using ShoppingApp.Services.DBServices.DBServiceInterfaces;
using ShoppingApp.Utils.InternalModels;

namespace ShoppingApp.CQRS.Handlers.CommandHandlers.CountryCommandHandlers
{
    public class UpdateCountryRangeStatusCommandHandler : IRequestHandler<UpdateCountryStatusRangeCommand, UpdateCountryStatusRangeResponseModel>
    {
        private readonly ICountryService _countryService;

        public UpdateCountryRangeStatusCommandHandler(ICountryService countryService)
        {
            _countryService = countryService;
        }

        public async Task<UpdateCountryStatusRangeResponseModel> Handle(UpdateCountryStatusRangeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var countries = (await _countryService.UpdateStatusRangeAsync(globalIds: request.GlobalIds, status: request.Status)).ToList();
                if (!(countries is null))
                {
                    if (countries.Count > 0)
                    {
                        return new UpdateCountryStatusRangeResponseModel
                        {
                            Countries = countries
                        };
                    }

                }
                return new UpdateCountryStatusRangeResponseModel
                {
                    HasError = true,
                    ErrorType = Utils.Enums.ErrorType.Model,
                    Errors = new List<InternalErrorModel>
                    {
                        new InternalErrorModel
                        {
                            Message = "Update failed"
                        }
                    }
                };
            }
            catch (System.Exception ex)
            {
                return new UpdateCountryStatusRangeResponseModel
                {
                    HasError = true,
                    ErrorType = Utils.Enums.ErrorType.Model,
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
