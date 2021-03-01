using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ShoppingApp.Web.UI.Areas.Shop.ViewModels
{
    public class UpdateAddressViewModel
    {
        [Required]
        public string AddressLine { get; set; }

        public string ZipCode { get; set; }

        [Required]
        public string SelectedCityId { get; set; }

        public SelectList Cities { get; set; }
    }
}
