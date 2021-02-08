using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ShoppingApp.CQRS.Models.QueryModels;
using ShoppingApp.CQRS.Models.ResponseModels.QueryResponseModels;
using ShoppingApp.Services.DBServices.DBServiceInterfaces;
using ShoppingApp.Utils.InternalModels;

namespace ShoppingApp.CQRS.Handlers.QueryHandlers
{
    public class GetPagedCategoriesQueryHandler : IRequestHandler<GetPagedCategoriesQuery, GetPagedCategoriesResponseModel>
    {
        private readonly ICategoryService _categoryService;
        public GetPagedCategoriesQueryHandler(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<GetPagedCategoriesResponseModel> Handle(GetPagedCategoriesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var categories = await _categoryService.GetPagedCategoriesAsync(
                    searchString: request.SearchString,
                    pageSize: request.PageSize,
                    pageNumber: request.PageNumber,
                    sortColumn: request.SortColumn,
                    sortDirection: request.SortDirection);
                if (categories.Data != null)
                {
                    return new GetPagedCategoriesResponseModel
                    {
                        Categories = categories
                    };
                }
                return new GetPagedCategoriesResponseModel
                {
                    HasError = true,
                    ErrorType = Utils.Enums.ErrorType.Model,
                    Errors = new List<InternalErrorModel>
                    {
                        new InternalErrorModel
                        {
                            Message = "Not Found"
                        }
                    }
                };
            }
            catch (Exception ex)
            {
                return new GetPagedCategoriesResponseModel
                {
                    HasError = true,
                    ErrorType = Utils.Enums.ErrorType.Exception,
                    Errors = new List<InternalErrorModel>
                    {
                        new InternalErrorModel
                        {
                            Message = ex.Message
                        }
                    }
                };
            }
        }
    }
}
