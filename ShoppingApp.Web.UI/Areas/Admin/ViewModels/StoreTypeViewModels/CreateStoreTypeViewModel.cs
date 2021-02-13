using System;
using System.ComponentModel.DataAnnotations;

namespace ShoppingApp.Web.UI.Areas.Admin.ViewModels.StoreTypeViewModels
{
    public class CreateStoreTypeViewModel
    {
        [Required]
        public string Name { get; set; }
    }
}
