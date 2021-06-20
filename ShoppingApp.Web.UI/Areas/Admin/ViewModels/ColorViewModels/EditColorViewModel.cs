using System.ComponentModel.DataAnnotations;

namespace ShoppingApp.Web.UI.Areas.Admin.ViewModels.ColorViewModels
{
    public class EditColorViewModel
    {
        [Required]
        public string Title { get; set; }

    }
}
