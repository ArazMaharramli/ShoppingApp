using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ShoppingApp.CQRS.Models.CommandModels.SizeCommands;
using ShoppingApp.CQRS.Models.ResponseModels.SizeResponseModels.CommandResponseModels;
using ShoppingApp.Domain.Models.Domain.ProductModels;
using ShoppingApp.Services.DBServices.DBServiceInterfaces;
using ShoppingApp.Utils.InternalModels;

namespace ShoppingApp.CQRS.Handlers.CommandHandlers.SizeCommandHandlers
{
    public class UpdateSizeCommandHandler : IRequestHandler<UpdateSizeCommand, UpdateSizeResponseModel>
    {
        private readonly ISizeService _sizeService;

        public UpdateSizeCommandHandler(ISizeService sizeService)
        {
            _sizeService = sizeService;
        }

        public async Task<UpdateSizeResponseModel> Handle(UpdateSizeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var sizeInDb = await _sizeService.FindByGobalIdAsync(globalId: request.GlobalId);
                if (!(sizeInDb is null))
                {
                    sizeInDb.UniqueTitle = request.Title.Trim();
                    sizeInDb = await _sizeService.UpdateAsync(sizeInDb);
                    return new UpdateSizeResponseModel
                    {
                        Size = sizeInDb
                    };
                }
                return new UpdateSizeResponseModel
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
                return new UpdateSizeResponseModel
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
