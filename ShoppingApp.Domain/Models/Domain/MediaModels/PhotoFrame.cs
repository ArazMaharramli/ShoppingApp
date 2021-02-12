using System.Collections.Generic;
using ShoppingApp.Domain.Models.Base;
using ShoppingApp.Domain.Models.Domain.StoreModels;
using ShoppingApp.Utils.Enums;

namespace ShoppingApp.Domain.Models.Domain.MediaModels
{
    public class PhotoFrame : BaseEntitySimple<Status>
    {
        public string FrameName { get; set; }
        public string FrameUrl { get; set; }

        //TRUE means assigned to store.
        //FALSE means public everyone can use
        public PhotoFrameAssignment IsAssignedToStore { get; set; } = PhotoFrameAssignment.Public;
        public ICollection<Store> Stores { get; set; } = new HashSet<Store>();

    }
}
