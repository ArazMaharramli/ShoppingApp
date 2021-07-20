using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShoppingApp.CQRS.Models.CommandModels.StoreCommands;
using ShoppingApp.CQRS.Models.QueryModels.CountryQueryModels;
using ShoppingApp.CQRS.Models.QueryModels.StoreTypeQueryModels;
using ShoppingApp.Domain.Models.Domain.StoreModels;
using ShoppingApp.Web.UI.ViewModels.OnboardingViewModels;

namespace ShoppingApp.Web.Controllers
{
    public class OnboardingController : Controller
    {
        private readonly IMediator _mediator;

        public OnboardingController(IMediator mediator)
        {
            _mediator = mediator;
        }
        // GET: /<controller>/
        public async Task<IActionResult> CreateShopAsync()
        {
            return View(await FillModelFields(new CreateShopViewModel()));
        }



        [HttpPost]
        public async Task<IActionResult> CreateShopAsync(CreateShopViewModel model)
        {
            var command = new CreateStoreCommand(
                name: model.Name,
                surname: model.Surname,
                email: model.Email,
                phoneNumber: model.PhoneNumber,
                storeName: model.StoreName,
                storeSlug: model.StoreSlug,
                storeStatus: Utils.Enums.StoreStatus.PendingConfirmation,
                storeTypeId: model.SelectedStoreType,
                storeEmail: model.StoreEmail,
                storePhone: model.StorePhone,
                cityId: model.SelectedCityId,
                address: model.Address,
                zipCode: model.ZipCode);
            var result = await _mediator.Send(command);
            if (!result.HasError)
            {
                return RedirectToAction("index", "Home", new { Area = "" });
            }
            if (result.ErrorType == Utils.Enums.ErrorType.Model)
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Message);
                }
            }
            return View(await FillModelFields(model));
        }

        #region MyRegion
        private async Task<CreateShopViewModel> FillModelFields(CreateShopViewModel model)
        {
            var cityRequest = new GetCountriesWithCitiesQuery();
            var cities = await _mediator.Send(cityRequest);

            var storeTypesRequest = new GetStoreTypesQuery();
            var storeTypes = await _mediator.Send(storeTypesRequest);

            var citySLI = new List<SelectListItem>();
            foreach (var item in cities.Countries)
            {
                citySLI.AddRange(item.Cities.Select(x => new SelectListItem { Text = x.Name, Value = x.GlobalId, Group = new SelectListGroup { Name = item.Name } }).ToList());
            }

            model.StoreTypes = new SelectList(storeTypes.StoreTypes, nameof(StoreType.GlobalId), nameof(StoreType.Name));
            model.Cities = new SelectList(citySLI, "Value", "Text", null, "Group.Name");
            return model;
        }
        #endregion
    }
}
