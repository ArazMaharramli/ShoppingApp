using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using MediatR;
using ShoppingApp.CQRS.Models.CommandModels.StoreCommands;
using ShoppingApp.CQRS.Models.ResponseModels.StoreResponseModels.CommandResponseModels;
using ShoppingApp.Services.DBServices.DBServiceInterfaces;
using ShoppingApp.Services.EmailServices;
using ShoppingApp.Utils.InternalModels;

namespace ShoppingApp.CQRS.Handlers.CommandHandlers.StoreCommandHandlers
{
    public class ConfirmStoreCommandHandler : IRequestHandler<ConfirmStoreCommand, UpdateStoreStatusResponseModel>
    {
        public IStoreService _storeService { get; set; }
        public IEmailSender _emailService { get; set; }
        public IUserIdentityService _userIdentityService { get; set; }

        public ConfirmStoreCommandHandler(IStoreService storeService, IEmailSender emailService, IUserIdentityService userIdentityService)
        {
            _storeService = storeService;
            _emailService = emailService;
            _userIdentityService = userIdentityService;
        }

        public async Task<UpdateStoreStatusResponseModel> Handle(ConfirmStoreCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _storeService.ConfirmStoreAsync(request.StoreId);
                if (!(response is null))
                {
                    var storeemail = response.StoreContacts.Where(x => x.ContactType == Utils.Enums.ContactType.Email).Select(x => x.Value).FirstOrDefault();
                    var code = await _userIdentityService.GeneratePasswordResetTokenAsync(response.Owner);
                    var encodedcode = HttpUtility.UrlEncode(code);
                    var callBackUrl = $"https://localhost:5005/Account/ResetPassword?userId={response.Owner.Id}&code={encodedcode}";

                    await _emailService.SendStoreCreatedEmailAsync(storeemail, $"{response.Owner.FirstName} {response.Owner.LastName}", callBackUrl);
                    return new UpdateStoreStatusResponseModel { Store = response };
                }
                return new UpdateStoreStatusResponseModel
                {
                    HasError = true,
                    ErrorType = Utils.Enums.ErrorType.Model,
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
