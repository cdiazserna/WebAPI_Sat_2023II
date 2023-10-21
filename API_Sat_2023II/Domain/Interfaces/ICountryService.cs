using API_Sat_2023II.DAL.Entities;
using System.Runtime.CompilerServices;

namespace API_Sat_2023II.Domain.Interfaces
{
    public interface ICountryService
    {
        Task<IEnumerable<Country>> GetCountriesAsync();
        Task<Country> CreateCountryAsync(Country country);
        Task<Country> GetCountryByIdAsync(Guid id);
        Task<Country> GetCountryByNameAsync(string name);
        Task<Country> EditCountryAsync(Country country);
        Task<Country> DeleteCountryAsync(Guid id);
    }
}
