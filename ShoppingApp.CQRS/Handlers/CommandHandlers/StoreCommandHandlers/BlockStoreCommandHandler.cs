using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ShoppingApp.CQRS.Models.CommandModels.StoreCommands;
using ShoppingApp.CQRS.Models.ResponseModels.StoreResponseModels.CommandResponseModels;
using ShoppingApp.Services.DBServices.DBServiceInterfaces;
using ShoppingApp.Services.EmailServices;
using ShoppingApp.Utils.InternalModels;

namespace ShoppingApp.CQRS.Handlers.CommandHandlers.StoreCommandHandlers
{
    public class BlockStoreCommandHandler : IRequestHandler<BlockStoreCommand, UpdateStoreStatusResponseModel>
    {
        public IStoreService _storeService { get; set; }
        public IEmailSender _emailService { get; set; }

        public BlockStoreCommandHandler(IStoreService storeService, IEmailSender emailService)
        {
            _storeService = storeService;
            _emailService = emailService;
        }

        public async Task<UpdateStoreStatusResponseModel> Handle(BlockStoreCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _storeService.BlockStoreAsync(request.StoreId);
                if (!(response is null))
                {
                    var storeemail = response.StoreContacts.Where(x => x.ContactType == Utils.Enums.ContactType.Email).Select(x => x.Value).FirstOrDefault();

                    await _emailService.SendStoreBlockedEmailAsync(storeemail, $"{response.Owner.FirstName} {response.Owner.LastName}");
                    return new UpdateStoreStatusResponseModel { Store = response };
                }
                return new UpdateStoreStatusResponseModel
                {
                    HasError = true,
                    ErrorType = Utils.Enums.ErrorType.Server,
                    Errors = new List<InternalErrorModel>
                        {
                            new InternalErrorModel
                            {
                                Message = "Store not found. Might be deleted."
                            }
                        }
                };
            }
            catch (System.Exception ex)
            {
                return new UpdateStoreStatusResponseModel
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
