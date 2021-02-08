﻿using MediatR;
using ShoppingApp.CQRS.Models.ResponseModels.QueryResponseModels;

namespace ShoppingApp.CQRS.Models.QueryModels
{
    public class GetPagedCategoriesQuery : IRequest<GetPagedCategoriesResponseModel>
    {
        public GetPagedCategoriesQuery(string searchString, int pageSize, int pageNumber, string sortColumn, string sortDirection)
        {
            SearchString = searchString;
            PageSize = pageSize;
            PageNumber = pageNumber;
            SortColumn = sortColumn;
            SortDirection = sortDirection;
        }

        public string SearchString { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public string SortColumn { get; set; }
        public string SortDirection { get; set; }

    }
}
