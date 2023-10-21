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

        public async Task<Country> GetCountryByIdAsync(Guid id)
        {
            //return await _context.Countries.FindAsync(id); // FindAsync es un método propio del DbContext (DbSet)
            //return await _context.Countries.FirstAsync(x => x.Id == id); //FirstAsync es un método de EF CORE
            return await _context.Countries.FirstOrDefaultAsync(c => c.Id == id); //FirstOrDefaultAsync es un método de EF CORE
        }

        public async Task<Country> GetCountryByNameAsync(string name)
        {
            return await _context.Countries.FirstOrDefaultAsync(c => c.Name == name);
        }

        public async Task<Country> EditCountryAsync(Country country)
        {
            try
            {
                country.ModifiedDate = DateTime.Now;

                _context.Countries.Update(country); //El método Update que es de EF CORE me sirve para Actualizar un objeto
                await _context.SaveChangesAsync();

                return country;
            }
            catch (DbUpdateException dbUpdateException)
            {
                throw new Exception(dbUpdateException.InnerException?.Message ?? dbUpdateException.Message);
            }
        }

        public async Task<Country> DeleteCountryAsync(Guid id)
        {
            try
            {
                //Aquí, con el ID que traigo desde el controller, estoy recuperando el país que luego voy a eliminar.
                //Ese país que recupero lo guardo en la variable country
                var country = await _context.Countries.FirstOrDefaultAsync(c => c.Id == id);
                if (country == null) return null; //Si el país no existe, entonces me retorna un NULL

                _context.Countries.Remove(country);
                await _context.SaveChangesAsync();

                return country;
            }
            catch (DbUpdateException dbUpdateException)
            {
                throw new Exception(dbUpdateException.InnerException?.Message ?? dbUpdateException.Message);
            }
        }
    }
}
