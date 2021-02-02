using System;
using System.ComponentModel.DataAnnotations;

namespace ShoppingApp.Web.API.Contracts.RequestModels.V1
{
    public class LoginWithExternalProviderRequestModel
    {
        [Required]
        public string Token { get; set; }
    }
}
