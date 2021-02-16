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
    public class DeleteCountryCommandHandler : IRequestHandler<DeleteCountryCommand, DeleteCountryRangeResponseModel>
    {
        private readonly ICountryService _countryService;

        public DeleteCountryCommandHandler(ICountryService countryService)
        {
            _countryService = countryService;
        }
        public async Task<DeleteCountryRangeResponseModel> Handle(DeleteCountryCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var deletedCount = await _countryService.DeleteAsync(globalId: request.GlobalId);

                if (deletedCount > 0)
                {
                    return new DeleteCountryRangeResponseModel
                    {
                        DeletedCount = deletedCount
                    };
                }
                return new DeleteCountryRangeResponseModel
                {
                    HasError = true,
                    ErrorType = Utils.Enums.ErrorType.Model,
                    Errors = new List<InternalErrorModel>
                    {
                        new InternalErrorModel
                        {
                            Message = "Delete failed"
                        }
                    }
                };
            }
            catch (System.Exception ex)
            {
                return new DeleteCountryRangeResponseModel
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
