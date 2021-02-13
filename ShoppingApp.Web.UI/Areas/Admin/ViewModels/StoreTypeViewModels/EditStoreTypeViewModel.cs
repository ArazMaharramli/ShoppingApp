using System.ComponentModel.DataAnnotations;

namespace ShoppingApp.Web.UI.Areas.Admin.ViewModels.StoreTypeViewModels
{
    public class EditStoreTypeViewModel
    {
        [Required]
        public string Name { get; set; }
    }
}
