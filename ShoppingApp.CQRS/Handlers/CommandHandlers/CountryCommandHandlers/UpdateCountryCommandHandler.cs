using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ShoppingApp.CQRS.Models.CommandModels.CountryCommands;
using ShoppingApp.CQRS.Models.ResponseModels.CountryResponseModels.CommandResponseModels;
using ShoppingApp.Services.DBServices.DBServiceInterfaces;
using ShoppingApp.Utils.InternalModels;

namespace ShoppingApp.CQRS.Handlers.CommandHandlers.CountryCommandHandlers
{
    public class UpdateCountryCommandHandler : IRequestHandler<UpdateCountryCommand, UpdateCountryResponseModel>
    {
        private readonly ICountryService _countryService;

        public UpdateCountryCommandHandler(ICountryService countryService)
        {
            _countryService = countryService;
        }

        public async Task<UpdateCountryResponseModel> Handle(UpdateCountryCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var countryInDb = await _countryService.FindByGobalIdAsync(globalId: request.GlobalId);
                if (!(countryInDb is null))
                {
                    countryInDb = await _countryService.UpdateAsync(
                        globalId: request.GlobalId,
                        name: request.Name,
                        abbreviation: request.Abbreviation,
                        phoneNumberPrefix: request.PhoneNumberPrefix,
                        cities: request.CitieNames);

                    return new UpdateCountryResponseModel
                    {
                        Country = countryInDb
                    };
                }
                return new UpdateCountryResponseModel
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
            catch (System.Exception ex)
            {
                return new UpdateCountryResponseModel
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
