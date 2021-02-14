using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ShoppingApp.CQRS.Models.CommandModels.StoreTypeCommands;
using ShoppingApp.CQRS.Models.ResponseModels.StoreTypeResponseModels.CommandResponseModels;
using ShoppingApp.Domain.Models.Domain.ProductModels;
using ShoppingApp.Services.DBServices.DBServiceInterfaces;
using ShoppingApp.Utils.InternalModels;

namespace ShoppingApp.CQRS.Handlers.CommandHandlers.StoreTypeCommandHandlers
{
    public class UpdateStoreTypeCommandHandler : IRequestHandler<UpdateStoreTypeCommand, UpdateStoreTypeResponseModel>
    {
        private readonly IStoreTypeService _storeTypeService;

        public UpdateStoreTypeCommandHandler(IStoreTypeService storeTypeService)
        {
            _storeTypeService = storeTypeService;
        }

        public async Task<UpdateStoreTypeResponseModel> Handle(UpdateStoreTypeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var storeTypeInDb = await _storeTypeService.FindByGobalIdAsync(globalId: request.GlobalId);
                if (!(storeTypeInDb is null))
                {
                    IEnumerable<Category> children = new List<Category>();

                    storeTypeInDb.Name = request.Name.Trim();
                    storeTypeInDb = await _storeTypeService.UpdateAsync(storeTypeInDb);
                    return new UpdateStoreTypeResponseModel
                    {
                        StoreType = storeTypeInDb
                    };
                }
                return new UpdateStoreTypeResponseModel
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
                return new UpdateStoreTypeResponseModel
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
