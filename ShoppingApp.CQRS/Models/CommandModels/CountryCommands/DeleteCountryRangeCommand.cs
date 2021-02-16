using MediatR;
using ShoppingApp.CQRS.Models.ResponseModels.CountryResponseModels.CommandResponseModels;

namespace ShoppingApp.CQRS.Models.CommandModels.CountryCommands
{
    public class DeleteCountryRangeCommand : IRequest<DeleteCountryRangeResponseModel>
    {
        public DeleteCountryRangeCommand(string[] globalIds)
        {
            GlobalIds = globalIds;
        }

        public string[] GlobalIds { get; set; }
    }
}
