using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ShoppingApp.CQRS.Models.CommandModels.CountryCommands;
using ShoppingApp.CQRS.Models.QueryModels.CountryQueryModels;
using ShoppingApp.Utils.Enums;
using ShoppingApp.Web.UI.Areas.Admin.PagedResponseModels;
using ShoppingApp.Web.UI.Areas.Admin.ViewModels.CountryViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ShoppingApp.Web.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CountryController : Controller
    {
        private readonly IMediator _mediator;

        public CountryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetPagedCountries()
        {
            var pageNumber = int.Parse(Request.Form["pagination[page]"].FirstOrDefault() ?? "1");
            var pageSize = int.Parse(Request.Form["pagination[perpage]"].FirstOrDefault() ?? "10");
            var totalRecordCount = Request.Form["pagination[total]"].FirstOrDefault();
            var totalPageCount = Request.Form["pagination[pages]"].FirstOrDefault();
            var searchString = Request.Form["query[generalSearch]"].FirstOrDefault();
            var status = Request.Form["query[Status]"].FirstOrDefault();
            var sortColumn = Request.Form["sort[field]"].FirstOrDefault();
            var sortDirection = Request.Form["sort[sort]"].FirstOrDefault();


            var query = new GetPagedCountriesQuery(
                searchString: searchString,
                pageSize: pageSize,
                pageNumber: pageNumber,
                sortColumn: sortColumn,
                sortDirection: sortDirection,
                status: status);

            var response = await _mediator.Send(query);

            if (!response.HasError)
            {
                var model = new BasePagedResponseModel<CountryResponseModel>
                {
                    Meta = new Meta
                    {
                        Field = sortColumn,
                        Sort = sortDirection,
                        Page = pageNumber,
                        Pages = response.Countries.PageCount,
                        Perpage = pageSize,
                        Total = response.Countries.Total
                    },
                    data = response.Countries.Data.Select(x => new CountryResponseModel
                    {
                        GlobalId = x.GlobalId,
                        Name = x.Name,
                        Status = x.Status.ToString(),
                        AddedDate = x.AddedDate.ToString("dd/MM/yyyy"),
                    }).ToList()
                };
                return Ok(model);
            }
            return NotFound();
        }


        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCountryViewModel model)
        {
            if (ModelState.IsValid)
            {
                var command = new CreateCountryCommand(
                    name: model.Name,
                    abbreviation: model.Abbreviation,
                    phoneNumberPrefix: model.PhoneNumberPrefix,
                    citieNames: model.Cities);

                var response = await _mediator.Send(command);
                if (!response.HasError)
                {
                    return RedirectToAction("Index", "Country", new { Area = "Admin" });
                }
                if (response.ErrorType == ErrorType.Model)
                {
                    foreach (var item in response.Errors)
                    {
                        ModelState.AddModelError("", item.Message);
                    }
                }
            }
            return View(model);
        }



        [HttpGet("[area]/[controller]/[action]/{globalId}")]
        public async Task<IActionResult> Edit(string globalId)
        {
            var query = new GetCountryByIdQuery(globalId: globalId);
            var response = await _mediator.Send(query);

            var model = new EditCountryViewModel();

            if (!response.HasError)
            {
                model.Name = response.Country.Name;
                model.Abbreviation = response.Country.Abbreviation;
                model.PhoneNumberPrefix = response.Country.PhoneNumberPrefix;
                model.Cities = response.Country.Cities.Select(x => x.Name).ToArray();
                model.CitiesAsString = string.Join(", ", model.Cities);
            }

            return View(model);
        }
        [HttpPost("[area]/[controller]/[action]/{globalId}")]
        public async Task<IActionResult> Edit(string globalId, EditCountryViewModel model)
        {
            if (ModelState.IsValid)
            {
                var command = new UpdateCountryCommand(
                    globalId: globalId,
                    name: model.Name,
                    abbreviation: model.Abbreviation,
                    phoneNumberPrefix: model.PhoneNumberPrefix,
                    citieNames: model.Cities);
                var response = await _mediator.Send(command);

                if (!response.HasError)
                {
                    return RedirectToAction("Index", "Country", new { Area = "Admin" });
                }
                if (response.ErrorType == ErrorType.Model)
                {
                    foreach (var item in response.Errors)
                    {
                        ModelState.AddModelError("", item.Message);
                    }
                    return View(model);
                }
            }
            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> UpdateStatusRange(string[] globalIds, string status)
        {
            Status selectedStatus;
            switch (status.ToLower())
            {
                case ("active"):
                    selectedStatus = Status.Active;
                    break;
                case ("deleted"):
                    selectedStatus = Status.Deleted;
                    break;
                case ("hidden"):
                    selectedStatus = Status.Hidden;
                    break;
                default:
                    return BadRequest();
            }

            var command = new UpdateCountryStatusRangeCommand(globalIds: globalIds, status: selectedStatus);
            var response = await _mediator.Send(command);
            if (!response.HasError)
            {
                return Ok(new { Message = "Updated" });
            }
            if (response.ErrorType == ErrorType.Model)
            {
                return BadRequest(new { Message = response.Errors.FirstOrDefault().Message });
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> DeleteRange(string[] globalIds)
        {
            var command = new DeleteCountryRangeCommand(globalIds: globalIds);
            var response = await _mediator.Send(command);


            if (!response.HasError)
            {
                return Ok(new { Message = $"{response.DeletedCount} Store Types Deleted" });
            }
            if (response.ErrorType == ErrorType.Model)
            {
                return BadRequest(new { Message = response.Errors.FirstOrDefault().Message });
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string globalId)
        {
            var command = new DeleteCountryCommand(globalId: globalId);
            var response = await _mediator.Send(command);


            if (!response.HasError)
            {
                return Ok(new { Message = "Deleted" });
            }
            if (response.ErrorType == ErrorType.Model)
            {
                return BadRequest(new { Message = response.Errors.FirstOrDefault().Message });
            }
            return NotFound();
        }
    }
}
