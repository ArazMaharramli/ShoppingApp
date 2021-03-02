using System.Collections.Generic;
using ShoppingApp.Domain.Models.Base;
using ShoppingApp.Utils.Enums;

namespace ShoppingApp.Domain.Models.Domain.AddressModels
{
    public class Country : BaseEntitySimple<Status>
    {
        public string Name { get; set; }
        public string Abbreviation { get; set; }
        public string PhoneNumberPrefix { get; set; }

        public ICollection<City> Cities { get; set; } = new HashSet<City>();

    }
}
