using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ShoppingApp.CQRS.Models.QueryModels.StoreQueryModels;
using ShoppingApp.Web.UI.Areas.Admin.PagedResponseModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ShoppingApp.Web.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ShopController : Controller
    {
        private readonly IMediator _mediator;

        public ShopController(IMediator mediator)
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

    }
}
