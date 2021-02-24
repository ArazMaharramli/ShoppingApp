using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ShoppingApp.Web.UI.Areas.Shop.ViewModels.OnboardingViewModels
{
    public class CreateShopViewModel
    {
        // user
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string PhoneNumber { get; set; }


        //store
        [Required]
        public string StoreName { get; set; }
        [Required]
        public string StoreSlug { get; set; }
        [Required]
        public string SelectedStoreType { get; set; }
        public SelectList StoreTypes { get; set; }

        //store contact
        [Required]
        public string StoreEmail { get; set; }
        public string StorePhone { get; set; }


        //address
        [Required]
        public string SelectedCityId { get; set; }
        public SelectList Cities { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string ZipCode { get; set; }


    }
}
