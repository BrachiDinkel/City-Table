using City.Core.Models;
using City.Service;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace City.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly CityService _cityService;

        public CityController(CityService cityService)
        {
            _cityService = cityService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CityClass>>> GetCities()
        {
            var cities = await _cityService.GetAllCitiesAsync();
            return Ok(cities);
        }

        [HttpGet("{name}")]
        public async Task<ActionResult<CityClass>> GetCity(string name)
        {
            var city = await _cityService.GetCityByNameAsync(name);
            if (city == null)
            {
                return NotFound();
            }
            return Ok(city);
        }

        [HttpPost]
        public async Task<ActionResult> AddCity(CityClass city)
        {
            await _cityService.AddCityAsync(city);
            return CreatedAtAction(nameof(GetCity), new { name = city.Name }, city);
        }

        [HttpPut("{name}")]
        public async Task<ActionResult> UpdateCity(string name, CityClass city)
        {
            await _cityService.UpdateCityAsync(name, city);
            return NoContent();
        }


        [HttpDelete("{name}")]
        public async Task<ActionResult> DeleteCity(string name)
        {
            await _cityService.DeleteCityAsync(name);
            return NoContent();
        }
    }
}
