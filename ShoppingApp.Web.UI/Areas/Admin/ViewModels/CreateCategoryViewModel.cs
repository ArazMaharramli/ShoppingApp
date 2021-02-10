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
    public class EditCategoryViewModel
    {
        [Required]
        public string CategoryName { get; set; }

        [Required]
        public string CategorySlug { get; set; }

        public string SelectedParentCategoryId { get; set; }
        public string[] SelectedChildrenCategoryIds { get; set; }

        public SelectList Categories { get; set; }
    }
}
