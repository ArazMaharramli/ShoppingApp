using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShoppingApp.CQRS.Models.CommandModels.StoreCommands;
using ShoppingApp.CQRS.Models.QueryModels.CountryQueryModels;
using ShoppingApp.CQRS.Models.QueryModels.StoreQueryModels;
using ShoppingApp.CQRS.Models.QueryModels.StoreTypeQueryModels;
using ShoppingApp.CQRS.Models.ResponseModels.StoreResponseModels.CommandResponseModels;
using ShoppingApp.Domain.Models.Domain.StoreModels;
using ShoppingApp.Web.UI.Areas.Admin.PagedResponseModels;
using ShoppingApp.Web.UI.Areas.Admin.ViewModels.StoreViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ShoppingApp.Web.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class StoreController : Controller
    {
        private readonly IMediator _mediator;

        public StoreController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetPagedStores()
        {
            var pageNumber = int.Parse(Request.Form["pagination[page]"].FirstOrDefault() ?? "1");
            var pageSize = int.Parse(Request.Form["pagination[perpage]"].FirstOrDefault() ?? "10");
            var totalRecordCount = Request.Form["pagination[total]"].FirstOrDefault();
            var totalPageCount = Request.Form["pagination[pages]"].FirstOrDefault();
            var searchString = Request.Form["query[generalSearch]"].FirstOrDefault();
            var status = Request.Form["query[Status]"].FirstOrDefault();
            var sortColumn = Request.Form["sort[field]"].FirstOrDefault();
            var sortDirection = Request.Form["sort[sort]"].FirstOrDefault();


            var query = new GetPagedStoresQuery(
                searchString: searchString,
                pageSize: pageSize,
                pageNumber: pageNumber,
                sortColumn: sortColumn,
                sortDirection: sortDirection,
                status: status);

            var response = await _mediator.Send(query);

            if (!response.HasError)
            {
                var model = new BasePagedResponseModel<ShopResponseModel>
                {
                    Meta = new Meta
                    {
                        Field = sortColumn,
                        Sort = sortDirection,
                        Page = pageNumber,
                        Pages = response.Stores.PageCount,
                        Perpage = pageSize,
                        Total = response.Stores.Total
                    },
                    data = response.Stores.Data.Select(x => new ShopResponseModel
                    {
                        GlobalId = x.GlobalId,
                        Name = x.StoreName,
                        Status = x.Status.ToString(),
                        AddedDate = x.AddedDate.ToString("dd/MM/yyyy"),
                    }).ToList()
                };
                return Ok(model);
            }
            return NotFound();
        }

        [HttpGet("[area]/[controller]/[action]/{storeId}")]
        public async Task<IActionResult> Edit(string storeId)
        {
            var query = new GetStoreByIdQuery(storeId);
            var response = await _mediator.Send(query);
            if (!response.HasError)
            {
                var model = new EditStoreViewModel
                {
                    OwnerName = response.Store.Owner.FirstName,
                    OwnerSurname = response.Store.Owner.LastName,
                    OwnerEmail = response.Store.Owner.Email,
                    OwnerPhoneNumber = response.Store.Owner.PhoneNumber,

                    StoreName = response.Store.StoreName,
                    StoreSlug = response.Store.UniqueSlug,
                    StoreContacts = response.Store.StoreContacts.Select(x => new StoreContactViewModel { ContactType = x.ContactType, Value = x.Value }).ToList(),
                    SelectedStoreType = response.Store.StoreType.GlobalId,

                    Address = response.Store.Address.AddressLine1,
                    ZipCode = response.Store.Address.ZipCode,
                    SelectedCityId = response.Store.Address.City.GlobalId,
                    IsBlocked = response.Store.Status == Utils.Enums.StoreStatus.NotConfirmed,
                    IsConfirmed = response.Store.Status == Utils.Enums.StoreStatus.Confirmed
                };
                return View(await FillModelFields(model));
            }
            return RedirectToAction("Error", "Home", new { Area = "" });

        }

        [HttpPost("[area]/[controller]/[action]/{storeId}")]
        public async Task<IActionResult> EditAsync(string storeId, EditStoreViewModel model)
        {
            if (ModelState.IsValid)
            {
                var response = new UpdateStoreStatusResponseModel();
                if (model.Status == Utils.Enums.StoreStatus.Confirmed)
                {
                    var command = new ConfirmStoreCommand(storeId: storeId);
                    response = await _mediator.Send(command);
                }
                if (model.Status == Utils.Enums.StoreStatus.NotConfirmed)
                {
                    var command = new BlockStoreCommand(storeId: storeId);
                    response = await _mediator.Send(command);
                }
                if (!response.HasError)
                {
                    return RedirectToAction("Index", "Store", new { Area = "Admin" });
                }
                if (response.ErrorType != Utils.Enums.ErrorType.Model)
                {
                    return RedirectToAction("Error", "Home", new { Area = "" });
                }
                if (response.ErrorType == Utils.Enums.ErrorType.Model)
                {
                    foreach (var item in response.Errors)
                    {
                        ModelState.AddModelError("", item.Message);
                    }
                    return View(await FillModelFields(model));
                }
            }
            return View(await FillModelFields(model));
        }
        #region MyRegion
        private async Task<EditStoreViewModel> FillModelFields(EditStoreViewModel model)
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
