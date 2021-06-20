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
    public class UpdateColorRangeStatusCommandHandler : IRequestHandler<UpdateColorStatusRangeCommand, UpdateColorStatusRangeResponseModel>
    {
        private readonly IColorService _colorService;

        public UpdateColorRangeStatusCommandHandler(IColorService colorService)
        {
            _colorService = colorService;
        }

        public async Task<UpdateColorStatusRangeResponseModel> Handle(UpdateColorStatusRangeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var colors = await _colorService.UpdateStatusRangeAsync(globalIds: request.GlobalIds, status: request.Status);
                if (!(colors is null))
                {
                    if (colors.Count > 0)
                    {
                        return new UpdateColorStatusRangeResponseModel
                        {
                            Colors = colors
                        };
                    }

                }
                return new UpdateColorStatusRangeResponseModel
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
                return new UpdateColorStatusRangeResponseModel
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
