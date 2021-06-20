using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShoppingApp.CQRS.Models.CommandModels.StoreCommands;
using ShoppingApp.CQRS.Models.QueryModels.CountryQueryModels;
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


        public async Task<IActionResult> Index()
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
                StoreEmail = contacts.Where(x => x.ContactType == Utils.Enums.ContactType.Email).FirstOrDefault()?.Value,
                StorePhoneNumber = contacts.Where(x => x.ContactType == Utils.Enums.ContactType.Phone).FirstOrDefault()?.Value,
                Address = response.Store.Address.AddressLine1
            };
            return View(model);
        }

        [HttpPost("[area]/[controller]/[action]/")]
        public async Task<IActionResult> UploadProfilePhotoAsync(UpdateProfilePhotoViewModel model)
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

        public async Task<IActionResult> Edit()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var query = new GetStoreByOwnerIdQuery(ownerId: user.Id);
            var response = await _mediator.Send(query);

            var contacts = response.Store.StoreContacts.Where(x => x.Status == Utils.Enums.Status.Active).ToList();
            var model = new UpdateStoreProfileViewModel
            {
                StoreName = response.Store.StoreName,
                StoreSlug = response.Store.UniqueSlug,
                Description = response.Store.Description,
                FacebookUrl = response.Store.FacebookUrl,
                InstagramUrl = response.Store.InstagramUrl,
                ProfilePhotoUrl = response.Store.ProfilePhotoUrl,
                Email = contacts.Where(x => x.ContactType == Utils.Enums.ContactType.Email).FirstOrDefault()?.Value,
                PhoneNumber = contacts.Where(x => x.ContactType == Utils.Enums.ContactType.Phone).FirstOrDefault()?.Value,
                Address = new UpdateAddressViewModel
                {
                    AddressLine = response.Store.Address.AddressLine1,
                    SelectedCityId = response.Store.Address.City.GlobalId,
                    ZipCode = response.Store.Address.ZipCode,
                }
            };
            return View(await FillModelFields(model));
        }

        [HttpPost]
        public async Task<IActionResult> EditAsync(UpdateStoreProfileViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(HttpContext.User);
                var command = new UpdateStoreProfileCommand(
                    ownerId: user.Id,
                    profilePictureFile: model.ProfilePhoto,
                    storeSlug: model.StoreSlug,
                    description: model.Description,
                    email: model.Email,
                    phoneNumber: model.PhoneNumber,
                    facebookUrl: model.FacebookUrl,
                    instagramUrl: model.InstagramUrl,
                    zipCode: model.Address.ZipCode,
                    addressLine: model.Address.AddressLine,
                    selectedCityId: model.Address.SelectedCityId);
                var response = await _mediator.Send(command);
                if (!response.HasError)
                {
                    return RedirectToAction("Index", "Profile", new { Area = "Shop" });
                }
                if (response.ErrorType == Utils.Enums.ErrorType.Model)
                {
                    foreach (var item in response.Errors)
                    {
                        ModelState.AddModelError("", item.Message);
                    }
                }
            }
            return View(await FillModelFields(model));
        }
        #region MyRegion
        private async Task<UpdateStoreProfileViewModel> FillModelFields(UpdateStoreProfileViewModel model)
        {
            var cityRequest = new GetCountriesWithCitiesQuery();
            var cities = await _mediator.Send(cityRequest);

            var citySLI = new List<SelectListItem>();
            foreach (var item in cities.Countries)
            {
                citySLI.AddRange(item.Cities.Select(x => new SelectListItem { Text = x.Name, Value = x.GlobalId, Group = new SelectListGroup { Name = item.Name } }).ToList());
            }

            model.Address.Cities = new SelectList(citySLI, "Value", "Text", null, "Group.Name");
            return model;
        }
        #endregion
    }
}
