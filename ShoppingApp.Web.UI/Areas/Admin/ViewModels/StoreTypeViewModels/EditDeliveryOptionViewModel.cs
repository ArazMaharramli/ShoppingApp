using System.ComponentModel.DataAnnotations;

namespace ShoppingApp.Web.UI.Areas.Admin.ViewModels.DeliveryOptionViewModels
{
    public class EditDeliveryOptionViewModel
    {
        [Required]
        public string Name { get; set; }


        [Required]
        public string Description { get; set; }
    }
}
