using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShoppingApp.CQRS.Models.CommandModels.CategoryCommands;
using ShoppingApp.CQRS.Models.QueryModels.CategoryQueryModels;
using ShoppingApp.Domain.Models.Domain.ProductModels;
using ShoppingApp.Utils.Enums;
using ShoppingApp.Web.UI.Areas.Admin.PagedResponseModels;
using ShoppingApp.Web.UI.Areas.Admin.ViewModels.CategoryViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ShoppingApp.Web.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly IMediator _mediator;

        public CategoryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetPagedCategories()
        {
            var pageNumber = int.Parse(Request.Form["pagination[page]"].FirstOrDefault() ?? "1");
            var pageSize = int.Parse(Request.Form["pagination[perpage]"].FirstOrDefault() ?? "10");
            var totalRecordCount = Request.Form["pagination[total]"].FirstOrDefault();
            var totalPageCount = Request.Form["pagination[pages]"].FirstOrDefault();
            var searchString = Request.Form["query[generalSearch]"].FirstOrDefault();
            var status = Request.Form["query[Status]"].FirstOrDefault();
            var sortColumn = Request.Form["sort[field]"].FirstOrDefault();
            var sortDirection = Request.Form["sort[sort]"].FirstOrDefault();


            var query = new GetPagedCategoriesQuery(
                searchString: searchString,
                pageSize: pageSize,
                pageNumber: pageNumber,
                sortColumn: sortColumn,
                sortDirection: sortDirection,
                status: status);

            var response = await _mediator.Send(query);

            if (!response.HasError)
            {
                var model = new BasePagedResponseModel<CategoryResponseModel>
                {
                    Meta = new Meta
                    {
                        Field = sortColumn,
                        Sort = sortDirection,
                        Page = pageNumber,
                        Pages = response.Categories.PageCount,
                        Perpage = pageSize,
                        Total = response.Categories.Total
                    },
                    data = response.Categories.Data.Select(x => new CategoryResponseModel
                    {
                        GlobalId = x.GlobalId,
                        UniqueName = x.UniqueName,
                        ParentName = x.Parent?.UniqueName,
                        Status = x.Status.ToString(),
                        AddedDate = x.AddedDate.ToString("dd/MM/yyyy"),
                    }).ToList()
                };
                return Ok(model);
            }
            return NotFound();
        }


        public async Task<IActionResult> Create()
        {
            var query = new GetCategoriesQuery();
            var response = await _mediator.Send(query);

            var model = new CreateCategoryViewModel();

            if (!response.HasError)
            {
                model.Categories = new SelectList(response.Categories, nameof(Category.GlobalId), nameof(Category.UniqueName));
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                var command = new CreateCategoryCommand(
                categoryName: model.CategoryName,
                slug: model.CategorySlug,
                parentId: model.SelectedParentCategoryId);

                var response = await _mediator.Send(command);
                if (!response.HasError)
                {
                    return RedirectToAction("Index", "Category", new { Area = "Admin" });
                }
                if (response.ErrorType == Utils.Enums.ErrorType.Model)
                {
                    foreach (var item in response.Errors)
                    {
                        ModelState.AddModelError("", item.Message);
                    }
                }
            }
            var query = new GetCategoriesQuery();
            var allCategoriesResponse = await _mediator.Send(query);

            if (!allCategoriesResponse.HasError)
            {
                model.Categories = new SelectList(allCategoriesResponse.Categories, nameof(Category.GlobalId), nameof(Category.UniqueName));
            }

            return View(model);
        }



        [HttpGet("[area]/[controller]/[action]/{globalId}")]
        public async Task<IActionResult> Edit(string globalId)
        {

            var query = new GetCategoriesQuery();
            var response = await _mediator.Send(query);

            var model = new EditCategoryViewModel();

            if (!response.HasError)
            {
                var getCategoryQuery = new GetCategoryByIdQuery(globalId: globalId);
                var getCategoryResponse = await _mediator.Send(getCategoryQuery);

                if (!getCategoryResponse.HasError)
                {
                    model.CategoryName = getCategoryResponse.Category.UniqueName;
                    model.CategorySlug = getCategoryResponse.Category.UniqueSlug;
                    model.SelectedChildrenCategoryIds = getCategoryResponse.Category.Children.Select(x => x.GlobalId).ToArray();
                    model.SelectedParentCategoryId = getCategoryResponse.Category.Parent?.GlobalId;
                }

                model.Categories = new SelectList(response.Categories.Where(x => x.GlobalId != globalId), nameof(Category.GlobalId), nameof(Category.UniqueName));
            }

            return View(model);
        }
        [HttpPost("[area]/[controller]/[action]/{globalId}")]
        public async Task<IActionResult> Edit(string globalId, EditCategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                var command = new UpdateCategoryCommand(
                    globalId: globalId,
                    name: model.CategoryName,
                    slug: model.CategorySlug,
                    parentId: model.SelectedParentCategoryId,
                    childrenIds: model.SelectedChildrenCategoryIds);
                var response = await _mediator.Send(command);

                if (!response.HasError)
                {
                    return RedirectToAction("Index", "Category", new { Area = "Admin" });
                }
                if (response.ErrorType == ErrorType.Model)
                {
                    foreach (var item in response.Errors)
                    {
                        ModelState.AddModelError("", item.Message);
                    }
                    return View(FillEditCategoryViewModelAsync(model, globalId));
                }
            }
            return View(FillEditCategoryViewModelAsync(model, globalId));
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

            var command = new UpdateCategoryStatusRangeCommand(globalIds: globalIds, status: selectedStatus);
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
            var command = new DeleteCategoryRangeCommand(globalIds: globalIds);
            var response = await _mediator.Send(command);


            if (!response.HasError)
            {
                return Ok(new { Message = $"{response.DeletedCount} Categories Deleted" });
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
            var command = new DeleteCategoryCommand(globalId: globalId);
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

        #region modelFillers
        private async Task<EditCategoryViewModel> FillEditCategoryViewModelAsync(EditCategoryViewModel model, string globalId)
        {
            var query = new GetCategoriesQuery();
            var response = await _mediator.Send(query);
            if (!response.HasError)
            {
                model.Categories = new SelectList(response.Categories.Where(x => x.GlobalId != globalId), nameof(Category.GlobalId), nameof(Category.UniqueName));
            }
            return model;

        }
        #endregion
    }

}
