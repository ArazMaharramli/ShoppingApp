using System.Collections.Generic;
using ShoppingApp.Domain.Models.Base;
using ShoppingApp.Utils.Enums;

namespace ShoppingApp.Domain.Models.Domain.AddressModels
{
    public class City : BaseEntitySimple<Status>
    {
        public string Name { get; set; }

        public long CountryId { get; set; }
        public Country Country { get; set; }

        public ICollection<Address> Addresses { get; set; } = new HashSet<Address>();
    }
}
