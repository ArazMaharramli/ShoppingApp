using MediatR;
using ShoppingApp.CQRS.Models.ResponseModels.CountryResponseModels.CommandResponseModels;
using ShoppingApp.Utils.Enums;

namespace ShoppingApp.CQRS.Models.CommandModels.CountryCommands
{
    public class UpdateCountryStatusRangeCommand : IRequest<UpdateCountryStatusRangeResponseModel>
    {
        public UpdateCountryStatusRangeCommand(string[] globalIds, Status status)
        {
            GlobalIds = globalIds;
            Status = status;
        }

        public string[] GlobalIds { get; set; }
        public Status Status { get; set; }
    }
}
