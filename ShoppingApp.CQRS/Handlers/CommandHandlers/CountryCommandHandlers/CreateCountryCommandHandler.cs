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
    public class CreateCountryCommandHandler : IRequestHandler<CreateCountryCommand, CreateCountryResponseModel>
    {
        private readonly ICountryService _countryService;

        public CreateCountryCommandHandler(ICountryService countryService)
        {
            _countryService = countryService;
        }

        public async Task<CreateCountryResponseModel> Handle(CreateCountryCommand request, CancellationToken cancellationToken)
        {
            var countryName = request.Name.Trim();
            var countryInDb = await _countryService.FindByNameAndStatusAsync(countryName);
            if (countryInDb is null)
            {

                var Country = await _countryService.CreateAsync(
                    name: countryName,
                    abbreviation: request.Abbreviation.Trim(),
                    phoneNumberPrefix: request.PhoneNumberPrefix.Trim(),
                    cityNames: request.CitieNames);

                if (!(Country is null))
                {
                    return new CreateCountryResponseModel
                    {
                        Country = Country
                    };
                }
                return new CreateCountryResponseModel
                {
                    HasError = true,
                    ErrorType = Utils.Enums.ErrorType.Exception,
                };
            }
            return new CreateCountryResponseModel
            {
                HasError = true,
                ErrorType = Utils.Enums.ErrorType.Model,
                Errors = new List<InternalErrorModel>
                {
                    new InternalErrorModel
                    {
                        Message = "Already exists"
                    }
                }
            };
        }
    }
}
