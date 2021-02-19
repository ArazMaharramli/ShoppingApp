using System;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ShoppingApp.Web.UI.Areas.Shop.ViewModels.OnboardingViewModels
{
    public class CreateShopViewModel
    {
        // user 
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }


        //store
        public string StoreName { get; set; }
        public string StoreSlug { get; set; }

        public string SelectedStoreType { get; set; }
        public SelectList StoreTypes { get; set; }

        //store contact
        public string StoreEmail { get; set; }
        public string StorePhone { get; set; }


        //address
        public string SelectedCityId { get; set; }
        public SelectList Cities { get; set; }

        public string Address { get; set; }
        public string ZipCode { get; set; }


    }
}
