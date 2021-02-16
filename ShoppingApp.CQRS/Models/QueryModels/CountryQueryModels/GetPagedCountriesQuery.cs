using MediatR;
using ShoppingApp.CQRS.Models.ResponseModels.CountryResponseModels.QueryResponseModels;

namespace ShoppingApp.CQRS.Models.QueryModels.CountryQueryModels
{
    public class GetPagedCountriesQuery : IRequest<GetPagedCountriesResponseModel>
    {
        public GetPagedCountriesQuery(string searchString, int pageSize, int pageNumber, string sortColumn, string sortDirection, string status)
        {
            SearchString = searchString;
            PageSize = pageSize;
            PageNumber = pageNumber;
            SortColumn = sortColumn;
            SortDirection = sortDirection;
            Status = status;
        }

        public string SearchString { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public string SortColumn { get; set; }
        public string SortDirection { get; set; }
        public string Status { get; set; }

    }
}
