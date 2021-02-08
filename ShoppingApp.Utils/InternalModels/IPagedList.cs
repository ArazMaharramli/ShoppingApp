using System.Collections.Generic;

namespace ShoppingApp.Utils.InternalModels
{
    public interface IPagedList<T>
    {
        int PageNumber { get; }
        int PageSize { get; }
        int PageCount { get; }
        int Total { get; }
        IEnumerable<T> Data { get; }
    }
}
