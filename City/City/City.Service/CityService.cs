using City.Core.Models;
using City.Core.Repositories;

namespace City.Service
{
    public class CityService
    {
        private readonly ICityRepository _repository;

        public CityService(ICityRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<CityClass>> GetAllCitiesAsync()
        {
            return await _repository.GetAllCitiesAsync();
        }

        public async Task<CityClass> GetCityByNameAsync(string name)
        {
            return await _repository.GetCityByNameAsync(name);
        }

        public async Task AddCityAsync(CityClass city)
        {
            await _repository.AddCityAsync(city);
        }

        public async Task UpdateCityAsync(string currentName, CityClass updatedCity)
        {
            var city = await _repository.GetCityByNameAsync(currentName);
            if (city != null)
            {
                await _repository.DeleteCityAsync(currentName);
                await _repository.AddCityAsync(updatedCity);
            }
        }

        public async Task DeleteCityAsync(string name)
        {
            await _repository.DeleteCityAsync(name);
        }
    }
}
