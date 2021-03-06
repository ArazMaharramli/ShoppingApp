using System;
using System.ComponentModel.DataAnnotations;

namespace ShoppingApp.Web.UI.Areas.Admin.ViewModels.SizeViewModels
{
    public class CreateSizeViewModel
    {
        [Required]
        public string Title { get; set; }

    }
}
