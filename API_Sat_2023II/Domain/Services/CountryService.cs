using API_Sat_2023II.DAL;
using API_Sat_2023II.DAL.Entities;
using API_Sat_2023II.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API_Sat_2023II.Domain.Services
{
    public class CountryService : ICountryService
    {
        private readonly DataBaseContext _context;

        public CountryService(DataBaseContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Country>> GetCountriesAsync()
        {
            var countries = await _context.Countries.ToListAsync();
            return countries; //Aquí lo que hago es traerme todos los datos que tengo en mi tabla Countries.
        }

        public async Task<Country> CreateCountryAsync(Country country)
        {
            try
            {
                country.Id = Guid.NewGuid(); //Así se asigna automáticamente un ID a un nuevo registro
                country.CreatedDate = DateTime.Now;

                _context.Countries.Add(country); //Aquí estoy creando el objeto Country en el contexto de mi BD
                await _context.SaveChangesAsync(); //Aquí ya estoy yendo a la BD para hacer el INSERT en la tabla Countries

                return country;
            }
            catch (DbUpdateException dbUpdateException)
            {
                //Esta exceptión me captura un mensaje cuando el país YA EXISTE (Duplicados)
                throw new Exception(dbUpdateException.InnerException?.Message ?? dbUpdateException.Message); //Coallesences Notation --> ??
            }
        }
    }
}
