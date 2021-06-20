using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ShoppingApp.CQRS.Models.CommandModels.ColorCommands;
using ShoppingApp.CQRS.Models.ResponseModels.ColorResponseModels.CommandResponseModels;
using ShoppingApp.Services.DBServices.DBServiceInterfaces;
using ShoppingApp.Utils.InternalModels;

namespace ShoppingApp.CQRS.Handlers.CommandHandlers.ColorCommandHandlers
{
    public class DeleteColorCommandHandler : IRequestHandler<DeleteColorCommand, DeleteColorRangeResponseModel>
    {
        private readonly IColorService _colorService;

        public DeleteColorCommandHandler(IColorService colorService)
        {
            _colorService = colorService;
        }

        public async Task<DeleteColorRangeResponseModel> Handle(DeleteColorCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var deletedCount = await _colorService.DeleteAsync(globalId: request.GlobalId);
                if (deletedCount > 0)
                {
                    return new DeleteColorRangeResponseModel
                    {
                        DeletedCount = deletedCount
                    };
                }
                return new DeleteColorRangeResponseModel
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
                return new DeleteColorRangeResponseModel
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
