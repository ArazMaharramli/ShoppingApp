using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShoppingApp.CQRS.Models.CommandModels.ProductCommands;
using ShoppingApp.CQRS.Models.QueryModels.CategoryQueryModels;
using ShoppingApp.CQRS.Models.QueryModels.ColorQueryModels;
using ShoppingApp.CQRS.Models.QueryModels.StoreQueryModels;
using ShoppingApp.Domain.Models.Domain.ProductModels;
using ShoppingApp.Domain.Models.Domain.UserModels;
using ShoppingApp.Web.UI.Areas.Shop.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ShoppingApp.Web.UI.Areas.Shop.Controllers
{
    [Area("Shop")]
    public class ProductController : Controller
    {
        private readonly IMediator _mediator;
        private readonly UserManager<User> _userManager;

        public ProductController(IMediator mediator, UserManager<User> userManager)
        {
            _mediator = mediator;
            _userManager = userManager;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> CreateAsync()
        {
            return View(await FillModelFieldsAsync(new CreateProductViewModel()));
        }
        [HttpPost]
        public async Task<IActionResult> CreateAsync(CreateProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(HttpContext.User);
                var storeQuery = new GetStoreByOwnerIdQuery(ownerId: user.Id);
                var storeResponse = await _mediator.Send(storeQuery);

                var command = new CreateProductCommand(
                    store: storeResponse.Store,
                    productTitle: model.ProductTitle,
                    productSlug: model.ProductSlug,
                    productDescription: model.ProductDescription,
                    shortDescription: model.ProductDescription ?? " ",
                    categoryId: model.SelectedCategoryId,
                    sizes: model.Sizes.Select(
                        x =>
                            new ProductSize(
                                size: x.Size,
                                price: x.Price,
                                discountedPrice: x.DiscountedPrice,
                                stockQuantity: x.StockQuantity,
                                colorIds: x.SelectedColorIds)).ToList(),
                    images: model.Images,
                    tags: model.Tags);

                var response = await _mediator.Send(command);

                if (!response.HasError)
                {
                    return RedirectToAction("Index", "Profile");
                }

                foreach (var item in response.Errors)
                {
                    ModelState.AddModelError("", item.Message);
                }
                return View(await FillModelFieldsAsync(new CreateProductViewModel()));
            }
            return View(await FillModelFieldsAsync(new CreateProductViewModel()));
        }
        #region modelFillers
        private async Task<CreateProductViewModel> FillModelFieldsAsync(CreateProductViewModel model)
        {
            var categoryQuery = new GetCategoriesQuery();
            var categoryResponse = await _mediator.Send(categoryQuery);

            var colorQuery = new GetColorsQuery();
            var colorResponse = await _mediator.Send(colorQuery);

            if (!categoryResponse.HasError)
            {
                model.Categories = new SelectList(categoryResponse.Categories, nameof(Category.GlobalId), nameof(Category.UniqueName));
            }
            if (!colorResponse.HasError)
            {
                model.Colors = new SelectList(colorResponse.Colors, nameof(Color.GlobalId), nameof(Color.UniqueTitle));
            }
            return model;
        }
        #endregion
    }
}
