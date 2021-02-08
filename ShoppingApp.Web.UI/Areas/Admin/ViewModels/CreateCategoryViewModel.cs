using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ShoppingApp.Web.UI.Areas.Admin.ViewModels
{
    public class CreateCategoryViewModel
    {
        [Required]
        public string CategoryName { get; set; }

        [Required]
        public string CategorySlug { get; set; }

        public string SelectedParentCategoryId { get; set; }

        public SelectList Categories { get; set; }
    }
}
