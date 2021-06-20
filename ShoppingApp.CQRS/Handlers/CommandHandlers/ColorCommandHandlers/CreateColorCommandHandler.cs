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
    public class CreateColorCommandHandler : IRequestHandler<CreateColorCommand, CreateColorResponseModel>
    {
        private readonly IColorService _colorService;

        public CreateColorCommandHandler(IColorService colorService)
        {
            _colorService = colorService;
        }

        public async Task<CreateColorResponseModel> Handle(CreateColorCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var colorName = request.Title.Trim();
                var colorInDb = await _colorService.FindByTitleAndStatusAsync(colorName);
                if (colorInDb is null)
                {

                    var color = await _colorService.CreateAsync(colorName);

                    if (!(color is null))
                    {
                        return new CreateColorResponseModel
                        {
                            Color = color
                        };
                    }
                    return new CreateColorResponseModel
                    {
                        HasError = true,
                        ErrorType = Utils.Enums.ErrorType.Server,
                    };
                }
                return new CreateColorResponseModel
                {
                    HasError = true,
                    ErrorType = Utils.Enums.ErrorType.Model,
                    Errors = new List<InternalErrorModel>
                    {
                        new InternalErrorModel
                        {
                            Message = "Already exists"
                        }
                    }
                };
            }
            catch (System.Exception ex)
            {
                return new CreateColorResponseModel
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
