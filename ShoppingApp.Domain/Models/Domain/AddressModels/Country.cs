using System.Collections.Generic;
using ShoppingApp.Domain.Models.Base;
using ShoppingApp.Utils.Enums;
using ShoppingApp.Domain.Models.Domain.ProductModels;

namespace ShoppingApp.Domain.Models.Domain.AddressModels
{
    public class Country : BaseEntitySimple<Status>
    {
        public string Name { get; set; }
        public string Abbreviation { get; set; }
        public string PhoneNumberPrefix { get; set; }

        public ICollection<Brand> Brands { get; set; } = new HashSet<Brand>();
        public ICollection<City> Cities { get; set; } = new HashSet<City>();

    }
}
