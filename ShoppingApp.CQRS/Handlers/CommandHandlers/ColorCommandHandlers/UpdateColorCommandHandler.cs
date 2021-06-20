using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ShoppingApp.CQRS.Models.CommandModels.ColorCommands;
using ShoppingApp.CQRS.Models.ResponseModels.ColorResponseModels.CommandResponseModels;
using ShoppingApp.Domain.Models.Domain.ProductModels;
using ShoppingApp.Services.DBServices.DBServiceInterfaces;
using ShoppingApp.Utils.InternalModels;

namespace ShoppingApp.CQRS.Handlers.CommandHandlers.ColorCommandHandlers
{
    public class UpdateColorCommandHandler : IRequestHandler<UpdateColorCommand, UpdateColorResponseModel>
    {
        private readonly IColorService _colorService;

        public UpdateColorCommandHandler(IColorService colorService)
        {
            _colorService = colorService;
        }

        public async Task<UpdateColorResponseModel> Handle(UpdateColorCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var colorInDb = await _colorService.FindByGobalIdAsync(globalId: request.GlobalId);
                if (!(colorInDb is null))
                {
                    colorInDb.UniqueTitle = request.Title.Trim();
                    colorInDb = await _colorService.UpdateAsync(colorInDb);
                    return new UpdateColorResponseModel
                    {
                        Color = colorInDb
                    };
                }
                return new UpdateColorResponseModel
                {
                    HasError = true,
                    ErrorType = Utils.Enums.ErrorType.Model,
                    Errors = new List<InternalErrorModel>
                    {
                        new InternalErrorModel
                        {
                            Message = "Not Found"
                        }
                    }
                };
            }
            catch (System.Exception ex)
            {
                return new UpdateColorResponseModel
                {
                    HasError = true,
                    ErrorType = Utils.Enums.ErrorType.Exception,
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
