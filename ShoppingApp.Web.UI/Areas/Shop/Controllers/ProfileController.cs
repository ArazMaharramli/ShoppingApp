using System.Linq;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ShoppingApp.CQRS.Models.CommandModels.StoreCommands;
using ShoppingApp.CQRS.Models.QueryModels.StoreQueryModels;
using ShoppingApp.Domain.Models.Domain.UserModels;
using ShoppingApp.Web.UI.Areas.Shop.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ShoppingApp.Web.UI.Areas.Shop.Controllers
{
    [Area("Shop")]
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly IMediator _mediator;
        private readonly UserManager<User> _userManager;

        public ProfileController(IMediator mediator, UserManager<User> userManager)
        {
            _mediator = mediator;
            _userManager = userManager;
        }


        public async System.Threading.Tasks.Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var query = new GetStoreByOwnerIdQuery(ownerId: user.Id);
            var response = await _mediator.Send(query);
            var contacts = response.Store.StoreContacts.Where(x => x.Status == Utils.Enums.Status.Active).ToList();
            var model = new StoreProfileViewModel
            {
                StoreName = response.Store.StoreName,
                Description = response.Store.Description,
                FaceBookUrl = response.Store.FacebookUrl,
                InstagramUrl = response.Store.InstagramUrl,
                ProfilePhotoUrl = response.Store.ProfilePhotoUrl,
                StoreEmail = contacts.Where(x => x.ContactType == Utils.Enums.ContactType.Email).FirstOrDefault().Value,
                StorePhoneNumber = contacts.Where(x => x.ContactType == Utils.Enums.ContactType.Phone).FirstOrDefault().Value,
                Address = response.Store.Address.AddressLine1
            };
            return View(model);
        }

        [HttpPost("[area]/[controller]/[action]/")]
        public async System.Threading.Tasks.Task<IActionResult> UploadProfilePhotoAsync(UpdateProfilePhotoViewModel model)
        {
            if (model.ProfilePhoto is null)
            {
                return BadRequest();
            }
            var owner = await _userManager.GetUserAsync(HttpContext.User);
            var command = new UpdateStoreProfilePhotoCommand(ownerId: owner.Id, profilePictureFile: model.ProfilePhoto);
            var response = await _mediator.Send(command);
            if (!response.HasError)
            {
                return Ok(response.PhotoUrl);
            }
            return BadRequest();
        }
    }
}
