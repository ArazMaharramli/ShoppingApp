using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShoppingApp.CQRS.Models.QueryModels.CountryQueryModels;
using ShoppingApp.CQRS.Models.QueryModels.StoreTypeQueryModels;
using ShoppingApp.Domain.Models.Domain.AddressModels;
using ShoppingApp.Domain.Models.Domain.StoreModels;
using ShoppingApp.Web.UI.Areas.Shop.ViewModels.OnboardingViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ShoppingApp.Web.UI.Areas.Shop.Controllers
{
    [Area("Shop")]
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
            var cityRequest = new GetCountriesWithCitiesQuery();
            var cities = await _mediator.Send(cityRequest);
            if (cities.HasError)
            {
                return RedirectToAction("Error", "Home", new { Area = "" });
            }
            var storeTypesRequest = new GetStoreTypesQuery();
            var storeTypes = await _mediator.Send(storeTypesRequest);
            if (storeTypes.HasError)
            {
                return RedirectToAction("Error", "Home", new { Area = "" });
            }
            if (cities.HasError)
            {
                return RedirectToAction("Error", "Home", new { Area = "" });
            }
            var citySLI = new List<SelectListItem>();
            foreach (var item in cities.Countries)
            {
                citySLI.AddRange(item.Cities.Select(x => new SelectListItem { Text = x.Name, Value = x.GlobalId, Group = new SelectListGroup { Name = item.Name } }).ToList());
            }
            var model = new CreateShopViewModel
            {
                StoreTypes = new SelectList(storeTypes.StoreTypes, nameof(StoreType.GlobalId), nameof(StoreType.Name)),
                Cities = new SelectList(citySLI, "Value", "Text", null, "Group.Name")
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult CreateShop(CreateShopViewModel model)
        {
            return View();
        }

    }
}
