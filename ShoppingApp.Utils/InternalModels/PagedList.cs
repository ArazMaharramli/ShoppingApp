using System.Collections.Generic;

namespace ShoppingApp.Utils.InternalModels
{
    public class PagedList<T> : IPagedList<T>
    {
        public PagedList(IEnumerable<T> data, int total, int pageSize, int pageCount, int pageNumber)
        {
            Data = data;
            PageNumber = pageNumber;
            PageSize = pageSize;
            Total = total;
            PageCount = pageCount;
        }

        public IEnumerable<T> Data { get; protected set; }

        public int PageNumber { get; protected set; }

        public int PageSize { get; protected set; }

        public int Total { get; protected set; }

        public int PageCount { get; protected set; }
    }
}
