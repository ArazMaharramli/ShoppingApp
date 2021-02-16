using MediatR;
using ShoppingApp.CQRS.Models.ResponseModels.CountryResponseModels.CommandResponseModels;

namespace ShoppingApp.CQRS.Models.CommandModels.CountryCommands
{
    public class DeleteCountryCommand : IRequest<DeleteCountryRangeResponseModel>
    {
        public DeleteCountryCommand(string globalId)
        {
            GlobalId = globalId;
        }

        public string GlobalId { get; set; }
    }
}
