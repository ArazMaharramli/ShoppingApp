using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ShoppingApp.CQRS.Models.CommandModels.SizeCommands;
using ShoppingApp.CQRS.Models.ResponseModels.SizeResponseModels.CommandResponseModels;
using ShoppingApp.Services.DBServices.DBServiceInterfaces;
using ShoppingApp.Utils.InternalModels;

namespace ShoppingApp.CQRS.Handlers.CommandHandlers.SizeCommandHandlers
{
    public class DeleteSizeCommandHandler : IRequestHandler<DeleteSizeCommand, DeleteSizeRangeResponseModel>
    {
        private readonly ISizeService _sizeService;

        public DeleteSizeCommandHandler(ISizeService sizeService)
        {
            _sizeService = sizeService;
        }

        public async Task<DeleteSizeRangeResponseModel> Handle(DeleteSizeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var deletedCount = await _sizeService.DeleteAsync(globalId: request.GlobalId);
                if (deletedCount > 0)
                {
                    return new DeleteSizeRangeResponseModel
                    {
                        DeletedCount = deletedCount
                    };
                }
                return new DeleteSizeRangeResponseModel
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
                return new DeleteSizeRangeResponseModel
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
