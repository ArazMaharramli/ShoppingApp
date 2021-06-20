using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ShoppingApp.CQRS.Models.CommandModels.ColorCommands;
using ShoppingApp.CQRS.Models.QueryModels.ColorQueryModels;
using ShoppingApp.Utils.Enums;
using ShoppingApp.Web.UI.Areas.Admin.PagedResponseModels;
using ShoppingApp.Web.UI.Areas.Admin.ViewModels.ColorViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ShoppingApp.Web.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ColorController : Controller
    {
        private readonly IMediator _mediator;

        public ColorController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> GetPagedColors()
        {
            var pageNumber = int.Parse(Request.Form["pagination[page]"].FirstOrDefault() ?? "1");
            var pageColor = int.Parse(Request.Form["pagination[perpage]"].FirstOrDefault() ?? "10");
            var totalRecordCount = Request.Form["pagination[total]"].FirstOrDefault();
            var totalPageCount = Request.Form["pagination[pages]"].FirstOrDefault();
            var searchString = Request.Form["query[generalSearch]"].FirstOrDefault();
            var status = Request.Form["query[Status]"].FirstOrDefault();
            var sortColumn = Request.Form["sort[field]"].FirstOrDefault();
            var sortDirection = Request.Form["sort[sort]"].FirstOrDefault();


            var query = new GetPagedColorsQuery(
                searchString: searchString,
                pageColor: pageColor,
                pageNumber: pageNumber,
                sortColumn: sortColumn,
                sortDirection: sortDirection,
                status: status);

            var response = await _mediator.Send(query);

            if (!response.HasError)
            {
                var model = new BasePagedResponseModel<ColorResponseModel>
                {
                    Meta = new Meta
                    {
                        Field = sortColumn,
                        Sort = sortDirection,
                        Page = pageNumber,
                        Pages = response.Colors.PageCount,
                        Perpage = pageColor,
                        Total = response.Colors.Total
                    },
                    data = response.Colors.Data.Select(x => new ColorResponseModel
                    {
                        GlobalId = x.GlobalId,
                        Title = x.UniqueTitle,
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
        public async Task<IActionResult> Create(CreateColorViewModel model)
        {
            if (ModelState.IsValid)
            {
                var command = new CreateColorCommand(title: model.Title);

                var response = await _mediator.Send(command);
                if (!response.HasError)
                {
                    return RedirectToAction("Index", "Color", new { Area = "Admin" });
                }
                if (response.ErrorType == Utils.Enums.ErrorType.Model)
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
            var query = new GetColorByIdQuery(globalId: globalId);
            var response = await _mediator.Send(query);

            var model = new EditColorViewModel();

            if (!response.HasError)
            {
                model.Title = response.Color.UniqueTitle;
            }

            return View(model);
        }
        [HttpPost("[area]/[controller]/[action]/{globalId}")]
        public async Task<IActionResult> Edit(string globalId, EditColorViewModel model)
        {
            if (ModelState.IsValid)
            {
                var command = new UpdateColorCommand(
                    globalId: globalId,
                    title: model.Title);
                var response = await _mediator.Send(command);

                if (!response.HasError)
                {
                    return RedirectToAction("Index", "Color", new { Area = "Admin" });
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

            var command = new UpdateColorStatusRangeCommand(globalIds: globalIds, status: selectedStatus);
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
            var command = new DeleteColorRangeCommand(globalIds: globalIds);
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
            var command = new DeleteColorCommand(globalId: globalId);
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
