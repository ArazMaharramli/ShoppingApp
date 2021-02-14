using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ShoppingApp.CQRS.Models.CommandModels.DeliveryOptionCommands;
using ShoppingApp.CQRS.Models.QueryModels.DeliveryOptionQueryModels;
using ShoppingApp.Utils.Enums;
using ShoppingApp.Web.UI.Areas.Admin.PagedResponseModels;
using ShoppingApp.Web.UI.Areas.Admin.ViewModels.DeliveryOptionViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ShoppingApp.Web.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DeliveryOptionController : Controller
    {
        private readonly IMediator _mediator;

        public DeliveryOptionController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetPagedDeliveryOptions()
        {
            var pageNumber = int.Parse(Request.Form["pagination[page]"].FirstOrDefault() ?? "1");
            var pageSize = int.Parse(Request.Form["pagination[perpage]"].FirstOrDefault() ?? "10");
            var totalRecordCount = Request.Form["pagination[total]"].FirstOrDefault();
            var totalPageCount = Request.Form["pagination[pages]"].FirstOrDefault();
            var searchString = Request.Form["query[generalSearch]"].FirstOrDefault();
            var status = Request.Form["query[Status]"].FirstOrDefault();
            var sortColumn = Request.Form["sort[field]"].FirstOrDefault();
            var sortDirection = Request.Form["sort[sort]"].FirstOrDefault();


            var query = new GetPagedDeliveryOptionsQuery(
                searchString: searchString,
                pageSize: pageSize,
                pageNumber: pageNumber,
                sortColumn: sortColumn,
                sortDirection: sortDirection,
                status: status);

            var response = await _mediator.Send(query);

            if (!response.HasError)
            {
                var model = new BasePagedResponseModel<DeliveryOptionResponseModel>
                {
                    Meta = new Meta
                    {
                        Field = sortColumn,
                        Sort = sortDirection,
                        Page = pageNumber,
                        Pages = response.DeliveryOptions.PageCount,
                        Perpage = pageSize,
                        Total = response.DeliveryOptions.Total
                    },
                    data = response.DeliveryOptions.Data.Select(x => new DeliveryOptionResponseModel
                    {
                        GlobalId = x.GlobalId,
                        UniqueName = x.UniqueName,
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
        public async Task<IActionResult> Create(CreateDeliveryOptionViewModel model)
        {
            if (ModelState.IsValid)
            {
                var command = new CreateDeliveryOptionCommand(name: model.Name, description: model.Description);

                var response = await _mediator.Send(command);
                if (!response.HasError)
                {
                    return RedirectToAction("Index", "DeliveryOption", new { Area = "Admin" });
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

            var query = new GetDeliveryOptionsQuery();
            var response = await _mediator.Send(query);

            var model = new EditDeliveryOptionViewModel();

            if (!response.HasError)
            {
                var getDeliveryOptionQuery = new GetDeliveryOptionByIdQuery(globalId: globalId);
                var getDeliveryOptionResponse = await _mediator.Send(getDeliveryOptionQuery);

                if (!getDeliveryOptionResponse.HasError)
                {
                    model.Name = getDeliveryOptionResponse.DeliveryOption.UniqueName;
                    model.Description = getDeliveryOptionResponse.DeliveryOption.Description;
                }
            }

            return View(model);
        }
        [HttpPost("[area]/[controller]/[action]/{globalId}")]
        public async Task<IActionResult> Edit(string globalId, EditDeliveryOptionViewModel model)
        {
            if (ModelState.IsValid)
            {
                var command = new UpdateDeliveryOptionCommand(
                    globalId: globalId,
                    uniqueName: model.Name,
                    description: model.Description);
                var response = await _mediator.Send(command);

                if (!response.HasError)
                {
                    return RedirectToAction("Index", "DeliveryOption", new { Area = "Admin" });
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

            var command = new UpdateDeliveryOptionStatusRangeCommand(globalIds: globalIds, status: selectedStatus);
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
            var command = new DeleteDeliveryOptionRangeCommand(globalIds: globalIds);
            var response = await _mediator.Send(command);


            if (!response.HasError)
            {
                return Ok(new { Message = $"{response.DeletedCount} DeliveryOptions Deleted" });
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
            var command = new DeleteDeliveryOptionCommand(globalId: globalId);
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
