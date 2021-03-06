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
    public class DeleteSizeRangeCommandHandler : IRequestHandler<DeleteSizeRangeCommand, DeleteSizeRangeResponseModel>
    {
        private readonly ISizeService _sizeService;

        public DeleteSizeRangeCommandHandler(ISizeService sizeService)
        {
            _sizeService = sizeService;
        }

        public async Task<DeleteSizeRangeResponseModel> Handle(DeleteSizeRangeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var deletedCount = await _sizeService.DeleteRangeAsync(globalIds: request.GlobalIds);
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
