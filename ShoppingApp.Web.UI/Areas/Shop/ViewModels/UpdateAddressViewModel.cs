using Microsoft.AspNetCore.Mvc.Rendering;

namespace ShoppingApp.Web.UI.Areas.Shop.ViewModels
{
    public class UpdateAddressViewModel
    {
        public string AddressLine { get; set; }
        public string ZipCode { get; set; }
        public string SelectedCityId { get; set; }

        public SelectList Cities { get; set; }
    }
}
