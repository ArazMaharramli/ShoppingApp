using MediatR;
using ShoppingApp.CQRS.Models.ResponseModels.StoreResponseModels.CommandResponseModels;
using ShoppingApp.Utils.Enums;

namespace ShoppingApp.CQRS.Models.CommandModels.StoreCommands
{
    public class CreateStoreCommand : IRequest<CreateStoreResponseModel>
    {
        public CreateStoreCommand(
            string name, string surname,
            string email, string phoneNumber,
            string storeName, string storeSlug,
            StoreStatus storeStatus, string storeTypeId,
            string storeEmail, string storePhone,
            string cityId, string address, string zipCode)
        {
            Name = name;
            Surname = surname;
            Email = email;
            PhoneNumber = phoneNumber;
            StoreName = storeName;
            StoreSlug = storeSlug;
            StoreStatus = storeStatus;
            StoreTypeId = storeTypeId;
            StoreEmail = storeEmail;
            StorePhone = storePhone;
            CityId = cityId;
            Address = address;
            ZipCode = zipCode;
        }

        // user 
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }


        //store
        public string StoreName { get; set; }
        public string StoreSlug { get; set; }
        public StoreStatus StoreStatus { get; set; }
        public string StoreTypeId { get; set; }

        //store contact
        public string StoreEmail { get; set; }
        public string StorePhone { get; set; }


        //address
        public string CityId { get; set; }

        public string Address { get; set; }
        public string ZipCode { get; set; }
    }
}
