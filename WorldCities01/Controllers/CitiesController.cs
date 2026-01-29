using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WorldCities01.Data;
using WorldCities01.Models;

namespace WorldCities01.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitiesController : ControllerBase
    {
        private readonly WorldCities01Context _context;

        public CitiesController(WorldCities01Context context)
        {
            _context = context;
        }

        // GET: api/Cities
        [HttpGet]
        public async Task<ActionResult<IEnumerable<City>>> GetCity()
        {
            List<City> cities = await _context.City.ToListAsync();
            var orderedCities = cities.OrderByDescending(city => city.Population);
            return orderedCities.ToList();
        }

        // GET: api/Cities/5
        [HttpGet("{id}")]
        public async Task<ActionResult<City>> GetCity(int id)
        {
            var city = await _context.City.FindAsync(id);

            if (city == null)
            {
                return NotFound();
            }

            return city;
        }

        // POST: api/Cities
        [HttpPost]
        public async Task<IActionResult> PostCity(string name, string country, int population)
        {
            var city = new City
            {
                Name = name,
                Country = country,
                Population = population
            };
            _context.City.Add(city);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetCity", new { id = city.CityId }, city);
        }

        // POST: api/Cities/5
        [HttpPost("{id}")]
        public async Task<IActionResult> PostCity(int id, string name = "", string country = "", int population = 0)
        {
            var city = await _context.City.FindAsync(id);
            if (city == null)
            {
                return NotFound();
            }
            if (name != "")
                city.Name = name;
            if (country != "")
                city.Country = country;
            if (population >= 0)
                city.Population = population;
            _context.Entry(city).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CityExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
        }

        private bool CityExists(int id)
        {
            return _context.City.Any(e => e.CityId == id);
        }
    }
}
