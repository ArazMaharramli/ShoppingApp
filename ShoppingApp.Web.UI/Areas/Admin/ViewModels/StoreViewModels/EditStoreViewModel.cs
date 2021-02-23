using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShoppingApp.Utils.Enums;

namespace ShoppingApp.Web.UI.Areas.Admin.ViewModels.StoreViewModels
{
    public class EditStoreViewModel
    {
        // user 
        public string OwnerName { get; set; }
        public string OwnerSurname { get; set; }
        public string OwnerEmail { get; set; }
        public string OwnerPhoneNumber { get; set; }


        //store
        public string StoreName { get; set; }
        public string StoreSlug { get; set; }

        public string SelectedStoreType { get; set; }
        public SelectList StoreTypes { get; set; }

        //store contact
        public List<StoreContactViewModel> StoreContacts { get; set; }


        //address
        public string SelectedCityId { get; set; }
        public SelectList Cities { get; set; }

        public string Address { get; set; }
        public string ZipCode { get; set; }
    }
    public class StoreContactViewModel
    {
        public ContactType ContactType { get; set; }
        public string Value { get; set; }
    }
}
