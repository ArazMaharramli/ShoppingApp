using System.Collections.Generic;
using System.Threading.Tasks;
using ShoppingApp.Domain.Models.Domain.AddressModels;
using ShoppingApp.Utils.Enums;
using ShoppingApp.Utils.InternalModels;

namespace ShoppingApp.Services.DBServices.DBServiceInterfaces
{
    public interface ICountryService
    {
        Task<IEnumerable<Country>> GetAllAsync();
        Task<IEnumerable<Country>> GetAllActivesAsync();
        Task<IEnumerable<Country>> GetAllHiddenAsync();
        Task<Country> GetWithCitiesAsync(string countryId);
        Task<IEnumerable<City>> GetCitiesAsync(string countryId);
        Task<City> GetCityAsync(string cityId);


        Task<IPagedList<Country>> GetPagedAsync(string searchString, int pageSize, int pageNumber, string sortColumn, string sortDirection, string status);
        Task<IEnumerable<Country>> FindRangeAsync(string[] globalIds);

        Task<Country> FindByNameAndStatusAsync(string name, Status status = Status.Active);
        Task<Country> FindByGobalIdAsync(string globalId);

        Task<Country> CreateAsync(string name, string phoneNumberPrefix, string abbreviation, string[] cityNames);
        Task<Country> UpdateAsync(string globalId, string name, string abbreviation, string phoneNumberPrefix, string[] cities);

        Task<Country> AddCityAsync(string cityName, string countryId);
        Task<Country> AddCityRangeAsync(string[] cityNames, string countryId);

        Task<Country> RemoveCityAsync(string cityName, string countryId);
        Task<Country> RemoveCityRangeAsync(string[] cityNames, string countryId);

        Task<IEnumerable<Country>> UpdateStatusRangeAsync(string[] globalIds, Status status);

        Task<int> DeleteRangeAsync(string[] globalIds);
        Task<int> DeleteAsync(string globalId);
    }
}
