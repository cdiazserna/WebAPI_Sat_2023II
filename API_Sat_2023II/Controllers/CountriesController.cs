using API_Sat_2023II.DAL.Entities;
using API_Sat_2023II.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API_Sat_2023II.Controllers
{
    [ApiController]
    [Route("api/[controller]")] //Esta es la primera parte de la URL de esta API: URL = api/countries
    public class CountriesController : Controller
    {
        private readonly ICountryService _countryService;
        public CountriesController(ICountryService countryService)
        {
            _countryService = countryService;
        }

        //En un controlador los métodos cambian de nombre, y realmente se llaman ACCIONES (ACTIONS) - Si es una API, se denomina ENDPOINT.
        //Todo Endpoint retorna un ActionResult, significa que retorna el resultado de una ACCIÓN.

        [HttpGet, ActionName("Get")]
        [Route("Get")] //Aquí concateno la URL inicial: URL = api/countries/get
        public async Task<ActionResult<IEnumerable<Country>>> GetCountriesAsync()
        {
            var countries = await _countryService.GetCountriesAsync(); //Aquí estoy yendo a mi capa de Domain para traerme la lista de países

            //El método Any() significa si hay al menos un elemento.
            //El Método !Any() significa si no hay absoluta/ nada.
            if (countries == null || !countries.Any()) 
            {
                return NotFound(); //NotFound = 404 Http Status Code
            }

            return Ok(countries); //Ok = 200 Http Status Code
        }

        [HttpPost, ActionName("Create")]
        [Route("Create")]
        public async Task<ActionResult> CreateCountryAsync(Country country)
        {
            try
            {
                var createdCountry = await _countryService.CreateCountryAsync(country);

                if (createdCountry == null)
                {
                    return NotFound(); //NotFound = 404 Http Status Code
                }

                return Ok(createdCountry); //Retorne un 200 y el objeto Country
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("duplicate"))
                {
                    return Conflict(String.Format("El país {0} ya existe.", country.Name)); //Confilct = 409 Http Status Code Error
                }

                return Conflict(ex.Message);
            }
        }
    }
}
