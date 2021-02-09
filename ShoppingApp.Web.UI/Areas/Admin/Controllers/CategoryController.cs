﻿using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShoppingApp.CQRS.Models.CommandModels;
using ShoppingApp.CQRS.Models.QueryModels;
using ShoppingApp.Domain.Models.Domain.ProductModels;
using ShoppingApp.Web.UI.Areas.Admin.PagedResponseModels;
using ShoppingApp.Web.UI.Areas.Admin.ViewModels;

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
        public async Task<IActionResult> CreateAsync(CreateCategoryViewModel model)
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

    }

}
