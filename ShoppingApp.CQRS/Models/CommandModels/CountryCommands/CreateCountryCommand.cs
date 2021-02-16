using MediatR;
using ShoppingApp.CQRS.Models.ResponseModels.CountryResponseModels.CommandResponseModels;

namespace ShoppingApp.CQRS.Models.CommandModels.CountryCommands
{
    public class CreateCountryCommand : IRequest<CreateCountryResponseModel>
    {
        public CreateCountryCommand(string name, string abbreviation, string phoneNumberPrefix, string[] citieNames)
        {
            Name = name;
            Abbreviation = abbreviation;
            PhoneNumberPrefix = phoneNumberPrefix;
            CitieNames = citieNames;
        }

        public string Name { get; set; }
        public string Abbreviation { get; set; }
        public string PhoneNumberPrefix { get; set; }
        public string[] CitieNames { get; set; }
    }
}
