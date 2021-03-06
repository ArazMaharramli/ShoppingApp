using System.ComponentModel.DataAnnotations;

namespace ShoppingApp.Web.UI.Areas.Admin.ViewModels.SizeViewModels
{
    public class EditSizeViewModel
    {
        [Required]
        public string Title { get; set; }

    }
}
