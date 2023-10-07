using API_Sat_2023II.DAL.Entities;
using System.Runtime.CompilerServices;

namespace API_Sat_2023II.Domain.Interfaces
{
    public interface ICountryService
    {
        Task<IEnumerable<Country>> GetCountriesAsync(); //Una firma de método
        Task<Country> CreateCountryAsync(Country country);
    }
}
