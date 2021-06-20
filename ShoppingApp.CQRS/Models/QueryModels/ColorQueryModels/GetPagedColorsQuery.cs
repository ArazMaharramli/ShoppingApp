using MediatR;
using ShoppingApp.CQRS.Models.ResponseModels.ColorResponseModels.QueryResponseModels;

namespace ShoppingApp.CQRS.Models.QueryModels.ColorQueryModels
{
    public class GetPagedColorsQuery : IRequest<GetPagedColorsResponseModel>
    {
        public GetPagedColorsQuery(string searchString, int pageColor, int pageNumber, string sortColumn, string sortDirection, string status)
        {
            SearchString = searchString;
            PageColor = pageColor;
            PageNumber = pageNumber;
            SortColumn = sortColumn;
            SortDirection = sortDirection;
            Status = status;
        }

        public string SearchString { get; set; }
        public int PageColor { get; set; }
        public int PageNumber { get; set; }
        public string SortColumn { get; set; }
        public string SortDirection { get; set; }
        public string Status { get; set; }

    }
}
