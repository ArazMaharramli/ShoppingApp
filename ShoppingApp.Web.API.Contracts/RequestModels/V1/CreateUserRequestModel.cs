using System;
using System.ComponentModel.DataAnnotations;
using ShoppingApp.Utils.Enums;

namespace ShoppingApp.Web.API.Contracts.RequestModels.V1
{
    public class CreateUserRequestModel
    {
        [Required]
        public string FirsName { get; set; }
        [Required]
        public string Lastname { get; set; }

        [EmailAddress]
        public string Email { get; set; }
        public string PhoneNumber { get; set; }


        public UserLoginType UserLoginType { get; set; }
    }
}
