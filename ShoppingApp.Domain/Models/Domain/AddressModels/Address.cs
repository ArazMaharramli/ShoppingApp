using System;
using System.Collections.Generic;
using ShoppingApp.Domain.Models.Base;
using ShoppingApp.Utils.Enums;
using ShoppingApp.Domain.Models.Domain.StoreModels;
using ShoppingApp.Domain.Models.Domain.OrderModels;

namespace ShoppingApp.Domain.Models.Domain.AddressModels
{
    public class Address : BaseEntitySimple<Status>
    {
        
        public string ZipCode { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }

        public double Longtitude { get; set; }
        public double Latitude { get; set; }
        public string Description { get; set; }

        public long CityId { get; set; }
        public City City { get; set; }

        public ICollection<Store> Stores { get; set; } = new HashSet<Store>();
        public ICollection<Order> Orders { get; set; } = new HashSet<Order>();

    }
}
