using MediatR;
using ShoppingApp.CQRS.Models.ResponseModels.CountryResponseModels.CommandResponseModels;

namespace ShoppingApp.CQRS.Models.CommandModels.CountryCommands
{
    public class UpdateCountryCommand : IRequest<UpdateCountryResponseModel>
    {
        public UpdateCountryCommand(string globalId, string name, string abbreviation, string phoneNumberPrefix, string[] citieNames)
        {
            GlobalId = globalId;
            Name = name;
            Abbreviation = abbreviation;
            PhoneNumberPrefix = phoneNumberPrefix;
            CitieNames = citieNames;
        }
        public string GlobalId { get; set; }
        public string Name { get; set; }
        public string Abbreviation { get; set; }
        public string PhoneNumberPrefix { get; set; }
        public string[] CitieNames { get; set; }
    }
}
