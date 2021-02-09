using System.Collections.Generic;

namespace ShoppingApp.Web.UI.Areas.Admin.PagedResponseModels
{
    // used for kt datatables
    public class BasePagedResponseModel<T>
    {
        public Meta Meta { get; set; }
        public List<T> data { get; set; }
    }
    public class Meta
    {
        public int Page { get; set; }
        public int Pages { get; set; }
        public int Perpage { get; set; }
        public int Total { get; set; }
        public string Sort { get; set; }
        public string Field { get; set; }
    }

}
