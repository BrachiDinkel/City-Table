using City.Core.Models;
using City.Core.Repositories;
using City.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace City.Data.Repositories
{
    public class CityRepository : ICityRepository
    {
        private readonly DataContext _context;

        public CityRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CityClass>> GetAllCitiesAsync()
        {
            return await _context.Cities.ToListAsync();
        }

        public async Task<CityClass> GetCityByNameAsync(string name)
        {
            return await _context.Cities.FirstOrDefaultAsync(c => c.Name == name);
        }

        public async Task AddCityAsync(CityClass city)
        {
            _context.Cities.Add(city);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCityAsync(string currentName, CityClass updatedCity)
        {
            var city = await _context.Cities.FirstOrDefaultAsync(c => c.Name == currentName);
            if (city != null)
            {
                _context.Cities.Remove(city);
                await _context.SaveChangesAsync();
                _context.Cities.Add(updatedCity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteCityAsync(string name)
        {
            var city = await _context.Cities.FirstOrDefaultAsync(c => c.Name == name);
            if (city != null)
            {
                _context.Cities.Remove(city);
                await _context.SaveChangesAsync();
            }
        }
    }
}
