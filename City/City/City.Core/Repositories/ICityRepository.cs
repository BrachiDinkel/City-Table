using City.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace City.Core.Repositories
{
    public interface ICityRepository
    {
        Task<IEnumerable<CityClass>> GetAllCitiesAsync();
        Task<CityClass> GetCityByNameAsync(string name);
        Task AddCityAsync(CityClass city);
        Task UpdateCityAsync(string currentName, CityClass updatedCity);
        Task DeleteCityAsync(string name);
    }
}
