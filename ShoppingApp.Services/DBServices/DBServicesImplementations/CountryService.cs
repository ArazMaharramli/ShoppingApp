using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ShoppingApp.Domain.Models.Domain.AddressModels;
using ShoppingApp.Services.DBServices.DBServiceInterfaces;
using ShoppingApp.UnitOFWork.Repositories;
using ShoppingApp.Utils.Classes;
using ShoppingApp.Utils.Enums;
using ShoppingApp.Utils.InternalModels;

namespace ShoppingApp.Services.DBServices.DBServicesImplementations
{
    public class CountryService : ICountryService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CountryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Country> CreateAsync(string name, string phoneNumberPrefix, string abbreviation, string[] cityNames)
        {
            var country = new Country
            {
                Name = name,
                PhoneNumberPrefix = phoneNumberPrefix,
                Abbreviation = abbreviation,
                Cities = cityNames?.Select(x => new City { Name = x, Status = Status.Active }).ToList(),
                Status = Status.Active
            };
            _unitOfWork.Countries.Add(country);
            await _unitOfWork.SaveChangesAsync();
            return country;
        }

        public async Task<Country> AddCityAsync(string cityName, string countryId)
        {
            var country = await _unitOfWork.Countries.GetAsync(x => x.GlobalId == countryId);
            var city = country.Cities?.Where(x => x.Name.Trim().ToLower() == cityName.Trim().ToLower()).FirstOrDefault();
            if (!(country is null))
            {
                if (city is null)
                {
                    city = new City { Name = cityName, Status = Status.Active };
                    country.Cities.Add(city);
                }
                else
                {
                    city.Status = Status.Active;
                    city.UpdatedDate = LocalTime.Now();
                }

                _unitOfWork.Countries.Update(country);
                await _unitOfWork.SaveChangesAsync();
            }
            return country;
        }

        public async Task<Country> AddCityRangeAsync(string[] cityNames, string countryId)
        {
            var country = await _unitOfWork.Countries.GetAsync(x => x.GlobalId == countryId);
            if (!(country is null))
            {

                country.Cities.Union(
                    cityNames.Select(
                        name => new City
                        {
                            Name = name,
                            Status = Status.Active
                        })
                    .ToList());
                _unitOfWork.Countries.Update(country);
                await _unitOfWork.SaveChangesAsync();
            }
            return country;
        }



        public Task<int> DeleteAsync(string globalId)
        {
            var country = _unitOfWork.Countries.GetAsync(x => x.GlobalId == globalId).Result;
            if (!(country is null))
            {
                country.Status = Status.Deleted;
                _unitOfWork.Countries.Update(country);
                return _unitOfWork.SaveChangesAsync();
            }
            return Task.FromResult(0);
        }

        public async Task<int> DeleteRangeAsync(string[] globalIds)
        {
            var countries = await _unitOfWork.Countries.FindAsync(x => globalIds.Contains(x.GlobalId));
            if (!(countries is null))
            {
                foreach (var country in countries)
                {
                    country.Status = Status.Deleted;
                }
                _unitOfWork.Countries.UpdateRange(countries);
                return await _unitOfWork.SaveChangesAsync();
            }
            return 0;
        }

        public Task<Country> FindByGobalIdAsync(string globalId)
        {
            return _unitOfWork.Countries.GetWithAllNavigationsAsync(x => x.GlobalId == globalId);
        }

        public Task<Country> FindByNameAndStatusAsync(string name, Status status = Status.Active)
        {
            return _unitOfWork.Countries.GetWithAllNavigationsAsync(
                x => x.Name.ToLower() == name.Trim().ToLower() &&
                x.Status == status);
        }

        public Task<IEnumerable<Country>> FindRangeAsync(string[] globalIds)
        {
            return _unitOfWork.Countries.FindAsync(x => globalIds.Contains(x.GlobalId));
        }

        public Task<IEnumerable<Country>> GetAllAsync()
        {
            return _unitOfWork.Countries.GetAllAsync();
        }

        public Task<IEnumerable<Country>> GetAllHiddenAsync()
        {
            return _unitOfWork.Countries.FindAsync(x => x.Status == Status.Hidden);
        }

        public Task<IEnumerable<City>> GetCitiesAsync(string countryId)
        {
            var country = _unitOfWork.Countries.GetWithAllNavigationsAsync(x => x.GlobalId == countryId).Result;
            return Task.FromResult(country?.Cities.AsEnumerable());
        }

        public Task<IPagedList<Country>> GetPagedAsync(string searchString, int pageSize, int pageNumber, string sortColumn, string sortDirection, string status)
        {
            Expression<Func<Country, bool>> predicate;
            switch (status?.ToLower())
            {
                case ("active"):
                    predicate = x => x.Status == Status.Active;
                    break;
                case ("deleted"):
                    predicate = x => x.Status == Status.Deleted;
                    break;
                case ("hidden"):
                    predicate = x => x.Status == Status.Hidden;
                    break;
                default:
                    predicate = x => true;
                    break;
            }
            return _unitOfWork.Countries.GetPagedAsync(
                predicate: predicate,
                searchString: searchString,
                pageSize: pageSize,
                pageNumber: pageNumber,
                sortColumn: sortColumn,
                sortDirection: sortDirection);
        }

        public async Task<Country> GetWithCitiesAsync(string countryId)
        {
            var country = await _unitOfWork.Countries.GetWithAllNavigationsAsync(x => x.GlobalId == countryId);
            country.Cities = country.Cities.Where(x => x.Status == Status.Active).ToList();
            return country;
        }

        public async Task<Country> RemoveCityAsync(string cityName, string countryId)
        {
            var country = await _unitOfWork.Countries.GetWithAllNavigationsAsync(x => x.GlobalId == countryId);
            if (!(country is null))
            {
                country.Cities.Where(x => x.Name.ToLower() == cityName.Trim().ToLower()).FirstOrDefault().Status = Status.Deleted;
                _unitOfWork.Countries.Update(country);
                await _unitOfWork.SaveChangesAsync();
                return country;
            }
            return country;
        }

        public async Task<Country> RemoveCityRangeAsync(string[] cityNames, string countryId)
        {
            var country = await _unitOfWork.Countries.GetWithAllNavigationsAsync(x => x.GlobalId == countryId);
            if (!(country is null))
            {
                var cities = country.Cities.Where(x => cityNames.Any(y => y.Trim().ToLower() == x.Name.ToLower()));
                foreach (var city in cities)
                {
                    city.Status = Status.Deleted;
                }

                _unitOfWork.Cities.UpdateRange(cities);
                await _unitOfWork.SaveChangesAsync();
                return country;
            }
            return country;
        }

        public async Task<Country> UpdateAsync(string globalId, string name, string abbreviation, string phoneNumberPrefix, string[] cities)
        {
            var country = await _unitOfWork.Countries.GetWithAllNavigationsAsync(x => x.GlobalId == globalId);
            if (!(country is null))
            {
                country.Name = name.Trim();
                country.Abbreviation = abbreviation.Trim();
                country.PhoneNumberPrefix = phoneNumberPrefix.Trim();
                var deletedCities = country.Cities.Where(
                    x => !cities.Any(
                        y => y.Trim().ToLower() == x.Name.ToLower()
                        )
                    && x.Status == Status.Active
                    );
                foreach (var city in deletedCities)
                {
                    city.Status = Status.Deleted;
                    city.UpdatedDate = LocalTime.Now();
                }

                var activatedCities = country.Cities.Where(
                    x => cities.Any(
                        y => y.Trim().ToLower() == x.Name.ToLower()
                        )
                    && x.Status != Status.Active
                    );

                foreach (var city in activatedCities)
                {
                    city.Status = Status.Active;
                    city.UpdatedDate = LocalTime.Now();
                }

                var addedCities = cities.Where(
                    x => !country.Cities.Any(
                        y => y.Name.ToLower() == x.Trim().ToLower() && y.Status == Status.Active)
                    ).Select(
                        x => new City { Name = x, Status = Status.Active }
                    ).ToList();
                foreach (var city in addedCities)
                {
                    country.Cities.Add(city);
                }

                country.UpdatedDate = LocalTime.Now();
                _unitOfWork.Countries.Update(country);
                await _unitOfWork.SaveChangesAsync();
            }
            return country;
        }

        public async Task<IEnumerable<Country>> UpdateStatusRangeAsync(string[] globalIds, Status status)
        {
            var countries = await _unitOfWork.Countries.FindAsync(x => globalIds.Contains(x.GlobalId));
            foreach (var country in countries)
            {
                country.Status = status;
                country.UpdatedDate = LocalTime.Now();
            }
            _unitOfWork.Countries.UpdateRange(countries);
            await _unitOfWork.SaveChangesAsync();
            return countries;
        }

        public Task<IEnumerable<Country>> GetAllActivesAsync()
        {
            return _unitOfWork.Countries.FindWithAllNavigationsAsync(x => x.Status == Status.Active);
        }

        public Task<City> GetCityAsync(string cityId)
        {
            return _unitOfWork.Cities.GetAsync(x => x.GlobalId == cityId && x.Status == Status.Active);
        }
    }
}
