using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ShoppingApp.CQRS.Models.CommandModels.StoreTypeCommands;
using ShoppingApp.CQRS.Models.ResponseModels.StoreTypeResponseModels.CommandResponseModels;
using ShoppingApp.Services.DBServices.DBServiceInterfaces;
using ShoppingApp.Utils.InternalModels;

namespace ShoppingApp.CQRS.Handlers.CommandHandlers.CategoryCommandHandlers
{
    public class CreateStoreTypeCommandHandler : IRequestHandler<CreateStoreTypeCommand, CreateStoreTypeResponseModel>
    {
        private readonly IStoreTypeService _storeTypeService;

        public CreateStoreTypeCommandHandler(IStoreTypeService storeTypeService)
        {
            _storeTypeService = storeTypeService;
        }

        public async Task<CreateStoreTypeResponseModel> Handle(CreateStoreTypeCommand request, CancellationToken cancellationToken)
        {
            var categoryName = request.Name.Trim();
            var categoryInDb = await _storeTypeService.FindByNameAndStatusAsync(categoryName);
            if (categoryInDb is null)
            {

                var category = await _storeTypeService.CreateAsync(categoryName);

                if (!(category is null))
                {
                    return new CreateStoreTypeResponseModel
                    {
                        StoreType = category
                    };
                }
                return new CreateStoreTypeResponseModel
                {
                    HasError = true,
                    ErrorType = Utils.Enums.ErrorType.Exception,
                };
            }
            return new CreateStoreTypeResponseModel
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
    }
}
