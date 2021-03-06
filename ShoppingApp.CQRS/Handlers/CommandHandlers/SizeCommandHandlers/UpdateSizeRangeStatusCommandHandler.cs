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
    public class UpdateSizeRangeStatusCommandHandler : IRequestHandler<UpdateSizeStatusRangeCommand, UpdateSizeStatusRangeResponseModel>
    {
        private readonly ISizeService _sizeService;

        public UpdateSizeRangeStatusCommandHandler(ISizeService sizeService)
        {
            _sizeService = sizeService;
        }

        public async Task<UpdateSizeStatusRangeResponseModel> Handle(UpdateSizeStatusRangeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var sizes = await _sizeService.UpdateStatusRangeAsync(globalIds: request.GlobalIds, status: request.Status);
                if (!(sizes is null))
                {
                    if (sizes.Count > 0)
                    {
                        return new UpdateSizeStatusRangeResponseModel
                        {
                            Sizes = sizes
                        };
                    }

                }
                return new UpdateSizeStatusRangeResponseModel
                {
                    HasError = true,
                    ErrorType = Utils.Enums.ErrorType.Model,
                    Errors = new List<InternalErrorModel>
                    {
                        new InternalErrorModel
                        {
                            Message = "Update failed"
                        }
                    }
                };
            }
            catch (System.Exception ex)
            {
                return new UpdateSizeStatusRangeResponseModel
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
