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
    public class CreateSizeCommandHandler : IRequestHandler<CreateSizeCommand, CreateSizeResponseModel>
    {
        private readonly ISizeService _sizeService;

        public CreateSizeCommandHandler(ISizeService sizeService)
        {
            _sizeService = sizeService;
        }

        public async Task<CreateSizeResponseModel> Handle(CreateSizeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var sizeName = request.Title.Trim();
                var sizeInDb = await _sizeService.FindByTitleAndStatusAsync(sizeName);
                if (sizeInDb is null)
                {

                    var size = await _sizeService.CreateAsync(sizeName);

                    if (!(size is null))
                    {
                        return new CreateSizeResponseModel
                        {
                            Size = size
                        };
                    }
                    return new CreateSizeResponseModel
                    {
                        HasError = true,
                        ErrorType = Utils.Enums.ErrorType.Server,
                    };
                }
                return new CreateSizeResponseModel
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
                return new CreateSizeResponseModel
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
