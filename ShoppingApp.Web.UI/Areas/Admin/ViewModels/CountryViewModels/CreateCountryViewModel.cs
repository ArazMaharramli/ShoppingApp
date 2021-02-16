using System.ComponentModel.DataAnnotations;

namespace ShoppingApp.Web.UI.Areas.Admin.ViewModels.CountryViewModels
{
    public class CreateCountryViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Abbreviation { get; set; }

        [Required]
        public string PhoneNumberPrefix { get; set; }


        [Required]
        public string[] Cities { get; set; }
    }
}
