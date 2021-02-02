using System.Collections.Generic;
using ShoppingApp.Domain.Models.Base;
using ShoppingApp.Utils.Enums;

namespace ShoppingApp.Domain.Models.Domain.StoreModels
{
    public class StoreType : BaseEntitySimple<Status>
    {
        //topSeller, etc. private public
        // private olan bize aiddir photoframe elave ede bilek ve s. ucun nezerde tutulur.
        public string Name { get; set; }

        public ICollection<Store> Stores { get; set; } = new HashSet<Store>();
    }
}
