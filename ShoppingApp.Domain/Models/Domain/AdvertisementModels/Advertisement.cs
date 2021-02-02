using System;
using ShoppingApp.Domain.Models.Base;
using ShoppingApp.Utils.Enums;

namespace ShoppingApp.Domain.Models.Domain.AdvertisementModels
{
    public class Advertisement : BaseEntitySimple<Status>
    {
        public string PropmotionImageUrl { get; set; }
        public string AvertiserCompanyUrl { get; set; }
        public string RedirectUrl { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        
        //yeri tipi ve s.
    }
}
