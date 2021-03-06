using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ShoppingApp.CQRS.Models.CommandModels.SizeCommands;
using ShoppingApp.CQRS.Models.QueryModels.SizeQueryModels;
using ShoppingApp.Utils.Enums;
using ShoppingApp.Web.UI.Areas.Admin.PagedResponseModels;
using ShoppingApp.Web.UI.Areas.Admin.ViewModels.SizeViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ShoppingApp.Web.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SizeController : Controller
    {
        private readonly IMediator _mediator;

        public SizeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> GetPagedSizes()
        {
            var pageNumber = int.Parse(Request.Form["pagination[page]"].FirstOrDefault() ?? "1");
            var pageSize = int.Parse(Request.Form["pagination[perpage]"].FirstOrDefault() ?? "10");
            var totalRecordCount = Request.Form["pagination[total]"].FirstOrDefault();
            var totalPageCount = Request.Form["pagination[pages]"].FirstOrDefault();
            var searchString = Request.Form["query[generalSearch]"].FirstOrDefault();
            var status = Request.Form["query[Status]"].FirstOrDefault();
            var sortColumn = Request.Form["sort[field]"].FirstOrDefault();
            var sortDirection = Request.Form["sort[sort]"].FirstOrDefault();


            var query = new GetPagedSizesQuery(
                searchString: searchString,
                pageSize: pageSize,
                pageNumber: pageNumber,
                sortColumn: sortColumn,
                sortDirection: sortDirection,
                status: status);

            var response = await _mediator.Send(query);

            if (!response.HasError)
            {
                var model = new BasePagedResponseModel<SizeResponseModel>
                {
                    Meta = new Meta
                    {
                        Field = sortColumn,
                        Sort = sortDirection,
                        Page = pageNumber,
                        Pages = response.Sizes.PageCount,
                        Perpage = pageSize,
                        Total = response.Sizes.Total
                    },
                    data = response.Sizes.Data.Select(x => new SizeResponseModel
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
        public async Task<IActionResult> Create(CreateSizeViewModel model)
        {
            if (ModelState.IsValid)
            {
                var command = new CreateSizeCommand(title: model.Title);

                var response = await _mediator.Send(command);
                if (!response.HasError)
                {
                    return RedirectToAction("Index", "Size", new { Area = "Admin" });
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
            var query = new GetSizeByIdQuery(globalId: globalId);
            var response = await _mediator.Send(query);

            var model = new EditSizeViewModel();

            if (!response.HasError)
            {
                model.Title = response.Size.UniqueTitle;
            }

            return View(model);
        }
        [HttpPost("[area]/[controller]/[action]/{globalId}")]
        public async Task<IActionResult> Edit(string globalId, EditSizeViewModel model)
        {
            if (ModelState.IsValid)
            {
                var command = new UpdateSizeCommand(
                    globalId: globalId,
                    title: model.Title);
                var response = await _mediator.Send(command);

                if (!response.HasError)
                {
                    return RedirectToAction("Index", "Size", new { Area = "Admin" });
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

            var command = new UpdateSizeStatusRangeCommand(globalIds: globalIds, status: selectedStatus);
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
            var command = new DeleteSizeRangeCommand(globalIds: globalIds);
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
            var command = new DeleteSizeCommand(globalId: globalId);
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
